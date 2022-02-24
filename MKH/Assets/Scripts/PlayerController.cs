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
    private Rigidbody rigidbody;

    [SerializeField]
    private float moveSpeed = 1.0f;


    private bool isClimb = false;
    private bool isGround = true;

    CLIMBSTATE climbState = CLIMBSTATE.COUNT;
    Vector3 climbStartPosition = Vector3.zero;
    Vector3 climbTargetPosition = Vector3.zero;
    enum CLIMBSTATE
    { 
        WALL, CORNER, COUNT
    };


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        skillManager = GameObject.FindWithTag("SkillManager").GetComponent<SkillManager>();

        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();

        // 벽타기
        RaycastHit rayHitInfo = new RaycastHit();
        Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);
        bool isRaycastHit = Physics.Raycast(transform.position + offset, transform.forward, out rayHitInfo, 1.0f);
        if (true == isRaycastHit)
        {
            if (false == isClimb && true == rayHitInfo.collider.gameObject.CompareTag("Scene"))
            {
                climbState = CLIMBSTATE.WALL;
                isClimb = true;
            }
        }
        else
        {
            // 벽이 끝에 도달했으니 코너모드
            if(true == isClimb)
            {
                climbState = CLIMBSTATE.CORNER;
                climbStartPosition = transform.position;
                climbTargetPosition = transform.position + offset + transform.forward;
            }
        }

        if(climbState == CLIMBSTATE.CORNER)
        {
            //Vector3.Slerp(climbStartPosition, climbTargetPosition, 0.1f);
            rigidbody.AddForce(transform.forward + transform.up * 10.0f, ForceMode.VelocityChange);
            isClimb = false;
            climbState = CLIMBSTATE.COUNT;
        }
    }

    private void FixedUpdate()
    {
        if (true == isClimb && climbState == CLIMBSTATE.WALL)
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void CheckInput()
    {
        float horizontalInput         = Input.GetAxis("Horizontal");
        float verticalInput           = Input.GetAxis("Vertical");
        Vector3 horizontalMove       = Vector3.zero;
        Vector3 verticalMove         = Vector3.zero;

        if (0.0f < horizontalInput)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }

        horizontalMove = new Vector3(0.0f, 0.0f, Mathf.Abs(horizontalInput) * gameManager.GetPlayerDeltaTime() * moveSpeed);
        
        if (true == isClimb)
        {
            verticalMove = new Vector3(0.0f, verticalInput * gameManager.GetPlayerDeltaTime() * 3.0f, 0.0f);
        }

        transform.Translate(horizontalMove + verticalMove);

        if (true == Input.GetKeyDown(KeyCode.Space))
        {
            if (true == isGround || true == isClimb)
            {
                rigidbody.AddForce(Vector3.up * 300.0f, ForceMode.Impulse);
                isGround = false;
                isClimb = false;
            }
            else
            {
                if (skillManager.isSkillUnLocked(SkillManager.Skill.DoubleJump) && skillManager.IsActived(SkillManager.Skill.DoubleJump))
                {
                    skillManager.DeActivate(SkillManager.Skill.DoubleJump);
                    rigidbody.AddForce(Vector3.up * 300.0f, ForceMode.Impulse);
                }
            }
        }

        // 스킬
        if (true == Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(skill, transform.position, skill.transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(true == collision.gameObject.CompareTag("Scene"))
        {
            isGround = true;
            skillManager.Activate(SkillManager.Skill.DoubleJump);
        }
    }
}
