using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject skill;

    // Manager
    private GameManager gameManager;
    private SkillManager skillManager;

    // Components
    private CapsuleCollider playerCollider;
    private Rigidbody  playerRigidbody;

    [SerializeField]  private float moveSpeed = 1.0f;
    private Vector3 moveVector = Vector3.zero;

    private bool isClimb = false;
    private bool isClimbRayHit = false;
    private bool isGround = true;

    float verticalInput = 0f;

    CLIMBSTATE climbState = CLIMBSTATE.COUNT;

    enum CLIMBSTATE
    { 
        WALL, CORNER, COUNT
    };


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        skillManager = GameObject.FindWithTag("SkillManager").GetComponent<SkillManager>();

        playerCollider = GetComponent<CapsuleCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        CheckInput();

        if (climbState == CLIMBSTATE.CORNER)
        {
            playerRigidbody.AddForce(transform.forward + transform.up * 10.0f, ForceMode.VelocityChange);
            isClimb = false;
            climbState = CLIMBSTATE.COUNT;
        }
    }

    private void CheckState()
    {
        // 땅 검사
        RaycastHit hitInfo = new RaycastHit();
        bool isGroundRayHit = Physics.Raycast(transform.position, -Vector3.up, out hitInfo, playerCollider.height / 2f + 0.1f);
        
        if(true == isGroundRayHit)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        // 벽 타기
        RaycastHit rayHitInfo = new RaycastHit();
        Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
        isClimbRayHit = Physics.Raycast(transform.position + offset, transform.forward, out rayHitInfo, playerCollider.radius);

        if (true == isClimbRayHit)
        {
            if (false == isClimb && true == rayHitInfo.collider.gameObject.CompareTag("Scene"))
            {
                climbState = CLIMBSTATE.WALL;
                isClimb = true;

                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.useGravity = false;
            }
        }
        else
        {
            playerRigidbody.useGravity = true;
            int a = 0;
            // 벽이 위쪽 끝에 도달했으니 코너모드
            if (true == isClimb && moveVector.y > 0f)
            {
                climbState = CLIMBSTATE.CORNER;
            }
        }

        if (true == isGround || true == isClimb)
        {
            skillManager.Activate(SkillManager.Skill.DoubleJump);
        }
    }

    private void CheckInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMove = Vector3.zero;

        verticalInput           = Input.GetAxis("Vertical");
        Vector3 verticalMove         = Vector3.zero;

        if (false == isClimb)
        {
            HorizontalTurn(horizontalInput);
            HorizontalMove(horizontalInput, out horizontalMove);
        }
        else
        {
            // 방향 처리
            // 좌우 입력이 없으면, 벽으로 방향을 돌림
            if(0f == horizontalInput)
            {
                if (false == isClimbRayHit)
                {
                    Vector3 rotation = Vector3.zero;
                    rotation.y = (transform.eulerAngles.y == 0f) ? 180f : 0f;
                    transform.eulerAngles = rotation;
                }
                else
                {
                    verticalMove = new Vector3(0f, verticalInput * gameManager.GetPlayerDeltaTime() * 3f, 0f);
                }
            }
            else
            {
                HorizontalTurn(horizontalInput);
                if(true == isGround)
                {
                    HorizontalMove(horizontalInput, out horizontalMove);
                }
            }

            //temp += horizontalInput / 2.0f;
            //Vector3 lineStart = transform.position;
            //Vector3 lineEnd = transform.position + (Quaternion.AngleAxis(temp, Vector3.right) * transform.forward);
            //Debug.DrawLine(lineStart, lineEnd);

        }

        moveVector = horizontalMove + verticalMove;
        transform.Translate(moveVector);

        if (true == Input.GetKeyDown(KeyCode.Space))
        {
            if (true == isGround || true == isClimb)
            {
                Jump();
                isGround = false;
                isClimb = false;
            }
            else
            {
                if (skillManager.isSkillUnLocked(SkillManager.Skill.DoubleJump) && skillManager.IsActived(SkillManager.Skill.DoubleJump))
                {
                    skillManager.DeActivate(SkillManager.Skill.DoubleJump);
                    Jump();
                }
            }
        }

        // 스킬
        if (true == Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(skill, transform.position, skill.transform.rotation);
        }
    }

    private void HorizontalTurn(float horizontalInput)
    {
        // 방향 처리
        // 좌우 입력하면 즉시 방향을 돌림
        if (0.0f < horizontalInput)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else if (0.0f > horizontalInput)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }

    private void HorizontalMove(float horizontalInput, out Vector3 horizontalMove)
    {
        // 이동 벡터 계산
        horizontalMove = new Vector3(0.0f, 0.0f, Mathf.Abs(horizontalInput) * gameManager.GetPlayerDeltaTime() * moveSpeed);
    }
    
    private void Jump()
    {
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.AddForce(Vector3.up * 300.0f, ForceMode.Impulse);
    }
}
