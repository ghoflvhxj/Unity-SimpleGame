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
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D  playerRigidbody;

    [SerializeField]  private float moveSpeed = 1.0f;
    private Vector3 moveVector = Vector3.zero;

    private Vector2 climbRayOrigin = Vector3.zero;
    private Vector2 climbRayHitPoint = Vector3.zero;
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

        playerCollider = GetComponent<CapsuleCollider2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        CheckInput();

        if (climbState == CLIMBSTATE.CORNER)
        {
            playerRigidbody.AddForce(transform.right + transform.up * 200.0f, ForceMode2D.Impulse);
            climbState = CLIMBSTATE.COUNT;
        }
    }

    private void CheckState()
    {
        // �� �˻�
        bool isGroundRayHit = Physics2D.Raycast(transform.position, -Vector3.up, playerCollider.size.y / 2f + 0.1f);
        if(true == isGroundRayHit)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        // �� Ÿ��
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.maxDepth = 3;
        contactFilter.minDepth = -3;

        climbRayOrigin = transform.position + new Vector3(0f, -playerCollider.size.y / 3f);
        RaycastHit2D rayHitInfo = Physics2D.CapsuleCast(climbRayOrigin, playerCollider.size/10f, CapsuleDirection2D.Vertical, 0f, transform.right, playerCollider.size.x/2f + 0.1f);
        isClimbRayHit = (rayHitInfo.collider != null);
        if (true == isClimbRayHit)
        {
            climbRayHitPoint = rayHitInfo.point;
            if (Vector2.Dot(rayHitInfo.normal, (climbRayHitPoint - climbRayOrigin).normalized) < -0.8f)
            {
                Debug.DrawLine(climbRayOrigin, climbRayHitPoint);
                Debug.DrawLine(climbRayHitPoint, climbRayHitPoint + rayHitInfo.normal, Color.red);

                if (false == isClimb && true == rayHitInfo.collider.gameObject.CompareTag("Scene"))
                {
                    playerRigidbody.velocity = Vector3.zero;
                    playerRigidbody.gravityScale = 0f;

                    climbState = CLIMBSTATE.WALL;
                    isClimb = true;

                }
            }
        }
        else
        {
            playerRigidbody.gravityScale = 1f;

            // ���� ���� ���� ���������� �ڳʸ��
            if (true == isClimb && moveVector.y > 0f)
            {
                climbState = CLIMBSTATE.CORNER;
            }
            else
            {
                climbState = CLIMBSTATE.COUNT;
            }
            isClimb = false;
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
            // ���� ó��
            // �¿� �Է��� ������, ������ ������ ����
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

                if(Vector2.Distance(climbRayHitPoint, climbRayOrigin) >= playerCollider.size.x/2f + 0.1f)
                {
                    Vector3 directionToWall = new Vector3(climbRayHitPoint.x - transform.position.x, 0f, 0f).normalized;
                    HorizontalMove(isClimbRayHit == true ? 1f : 0f, out horizontalMove);
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

        // ��ų
        if (true == Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(skill, transform.position, skill.transform.rotation);
        }
    }

    private void HorizontalTurn(float horizontalInput)
    {
        // ���� ó��
        // �¿� �Է��ϸ� ��� ������ ����
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
        // �̵� ���� ���
        horizontalMove = new Vector3(Mathf.Abs(horizontalInput) * gameManager.GetPlayerDeltaTime() * moveSpeed, 0f, 0f);
    }
    
    private void Jump()
    {
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.AddForce(Vector3.up * 300.0f, ForceMode2D.Impulse);
    }
}
