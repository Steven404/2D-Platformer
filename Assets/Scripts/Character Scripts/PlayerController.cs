using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int variableCheckJumpFix;

    private int facingDirection = 1;

    private float movementInputDirection;

    private bool isFacingRight = true;

    private bool isGrounded;

    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isWallHoping;

    private int amountOfJumpsLeft;

    [Header("For Tiles and Sprites")]
    [SerializeField] public GameObject doubleJumpCoin;
    [SerializeField] public GameObject youWinCoin;

    [Header("For ground movement")]
    [SerializeField] public float movementSpeed;
    [SerializeField] public Rigidbody2D rb;

    [Header("For Air Movement")]
    [SerializeField] public float airMovementForce;
    [SerializeField] public float airDragMultiplier = 0.95f;

    [Header("For Jumping")]
    [SerializeField] public float jumpRememberTime = 0.2f;
    [SerializeField] public float jumpForce = 10;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius;
    [SerializeField] public int amountOfJumps = 1;
    [SerializeField] public float variableJumpeHeightMultiplier = 0.5f;

    [Header("For Wall Jumping")]
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public Transform wallCheck;
    [SerializeField] public float wallCheckDistance;
    [SerializeField] public float wallSlideSpeed;
    [SerializeField] public Vector2 wallHopDirection;
    [SerializeField] public Vector2 wallJumpDirection;
    [SerializeField] public float wallHopForce;
    [SerializeField] public float wallJumpForce;

    [Header("For animation")]
    [SerializeField] public Animator animator;


    private float jumpRemember;

    // Start is called before the first frame update
    void Start()
    {
        isWallHoping = false;
        jumpRemember = jumpRememberTime;
        rb = GetComponent<Rigidbody2D>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("DoubleJumpCoin")) {
            amountOfJumps += 1;
            doubleJumpCoin.SetActive(false);
            LevelManagerScript.instance.djText = true;
        }
        if (other.gameObject.CompareTag("YouWinSquare")) {
            LevelManagerScript.instance.youWin();

        }
    }*/


    // Update is called once per frame
    void Update()
    {
    
        checkInput();
        checkMovementDirection();
        checkIfWallSliding();
        
    }

    private void FixedUpdate() {
        applyMovement();
        checkSurroundings();
        Vector3 whereIAm = Camera.main.transform.position;
        Vector3 whereIShouldBe = transform.position - new Vector3(0, 0, 10);
        Camera.main.transform.position = Vector3.Lerp(whereIAm, whereIShouldBe, 0.1f);
        //camera follow

    }

    public void checkSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void checkIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else isWallSliding = false;
    }


    public void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
    }

    private void checkMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0) Flip();
        else if (!isFacingRight && movementInputDirection > 0) Flip();
    }

    private void checkInput()
    {
        if (isGrounded)
        {
            variableCheckJumpFix = 1;
            amountOfJumpsLeft = amountOfJumps;
        }
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        jumpRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump")) jumpRemember = jumpRememberTime;
        if ((jumpRemember > 0 && isGrounded) || (isWallSliding && Input.GetButtonDown("Jump")) || (Input.GetButtonDown("Jump") && amountOfJumpsLeft>0))
        {
            jump();
        }
        if (Input.GetButtonUp("Jump") && !isGrounded && variableCheckJumpFix > 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y  * variableJumpeHeightMultiplier);
            variableCheckJumpFix--;
        }
        
        if (movementInputDirection != 0)
        {
            animator.SetBool("isRunning",true);
        } else if (movementInputDirection == 0)
        {
            animator.SetBool("isRunning", false);
        }
        if (!isGrounded) animator.SetBool("isJumping", true);
        if (isGrounded) animator.SetBool("isJumping", false);

    }

    private void jump()
    {
        if (!isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        } else if (isWallSliding && movementInputDirection == facingDirection)
        {
            isWallHoping = true;
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        } else if (isTouchingWall && !isGrounded && movementInputDirection != facingDirection)
        {
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            Flip();
            isWallHoping = false;
        }
    }

    private void applyMovement() {
        if (isGrounded) {
            isWallHoping = false;
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        } else if (!isGrounded && !isWallSliding && !isWallHoping && movementInputDirection !=0) {
            Vector2 forceToAdd = new Vector2(airMovementForce * movementInputDirection, rb.velocity.y);
            rb.AddForce(forceToAdd);

            if(Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }
        }
        else if (!isGrounded && !isWallSliding && movementInputDirection == 0) {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }


        if (isWallSliding) {
            if (rb.velocity.y < -wallSlideSpeed) {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void Flip() {
        if (!isWallSliding) {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
