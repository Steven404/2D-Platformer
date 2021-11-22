using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject player;

    private bool isFacingRight = true;
    private bool isJumping;
    private bool isFalling;
    private bool isRunning;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isWallJumping;
    //private bool isDashing;
    private bool canDoubleJump;
    public static bool canMove;

    private int facingDirection = 1;
    private int variableJumpCounter;
    private int amountOfJumpsLeft;

    private float groundRemember;
    private float jumpRemember;
    private float movementDirectionInput;
    private float wallJumpTimeLeft;
    //private float dashTimeLeft;
    //private float lastDash;

    private Animator animator;

    [Header("For Ground Movement")]
    [SerializeField] public float movementSpeed;
    [SerializeField] private Rigidbody2D rb;

    [Header("For Air Movement")]
    [SerializeField] public float airMovementForce;
    [SerializeField] public float airDragMultiplier;

    /* [Header("For Dashing")]
     public float dashTime;
     public float dashSpeed;
     public float dashCooldown; */

    [Header("For jumping")]
    [SerializeField] public float groundRemeberTime = 0.1f;
    [SerializeField] public float jumpRememberTime = 0.2f;
    [SerializeField] public float jumpForce;
    [SerializeField] public int amountOfJumps = 2;
    [SerializeField] public float groundCheckRadious;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public float variableJumpHeightMultiplier;

    [Header("For Walljump")]
    [SerializeField] public Transform wallCheck;
    [SerializeField] public float wallCheckDistance;
    [SerializeField] public float wallSlidingSpeed;
    [SerializeField] public float wallHopForce;
    [SerializeField] public Vector2 wallHopDirection;
    [SerializeField] public float wallJumpForce;
    [SerializeField] public Vector2 wallJumpDirection;
    [SerializeField] public float wallJumpTimer; // allowing the player to controller the player after a specified time


    void Start() {
        if (!SceneManager.GetActiveScene().name.Equals("Menu")) canMove = true;
        amountOfJumpsLeft = amountOfJumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckIfWallSliding();
        UpdateAnimations();
        CheckSurroundings();
        //CheckDash();
    }

    private void FixedUpdate() {
        ApplyMovement();
    }

    private void CheckInput() {
        movementDirectionInput = Input.GetAxisRaw("Horizontal");
        jumpRemember -= Time.deltaTime;
        wallJumpTimeLeft -= Time.deltaTime;
        groundRemember -= Time.deltaTime;

        if (isGrounded) {
            groundRemember = groundRemeberTime;
        }
        if (Input.GetButtonDown("Jump")) {
            jumpRemember = jumpRememberTime;
        }
        if (canMove && ((jumpRemember > 0 && groundRemember > 0) || ((isWallSliding) && jumpRemember > 0) || (jumpRemember > 0 && !isTouchingWall && canDoubleJump == true && amountOfJumpsLeft > 0))) {
            Jump();
        }
        if ((isWallSliding || isTouchingWall) && Input.GetButtonDown("Fire3")) {
            wallJumpTimeLeft = wallJumpTimer;
            isWallJumping = true;
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            flip();
        }
        if (Input.GetButtonUp("Jump") && variableJumpCounter > 0) {
            if (rb.velocity.y > 0) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
                variableJumpCounter--; // Variable Jump bug fix (slowing down on spamming space when falling)
            }
        }
       /* if (Input.GetButtonDown("Fire3")) {
            if (Time.time >= lastDash + dashCooldown) {
                AttemptToDash();
            }
        } */
    }

   /* private void AttemptToDash() {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
    } */

   /* private void CheckDash() {
        if (isDashing) {
            if (dashTimeLeft > 0) {
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;
            }
            if (dashTimeLeft <= 0 || isTouchingWall) {
                isDashing = false;
            }
        }
    } */

    private void CheckIfWallSliding() {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0) {
            isWallSliding = true;
        } else {
            isWallSliding = false;
        }
    }

    private void CheckMovementDirection() {
        if(isFacingRight && movementDirectionInput < 0 && !isWallJumping && canMove) {
            flip();
        } else if(!isFacingRight && movementDirectionInput > 0 && !isWallJumping && canMove) {
            flip();
        }
        if(isGrounded && rb.velocity.y <= 0.01f && movementDirectionInput !=0 && canMove) {
            isRunning = true;
        } 
        else {
            isRunning = false;
        }
        if (rb.velocity.y > 0.01 && !isGrounded && canMove) {
            isJumping = true;
        }
        else isJumping = false;
        if (rb.velocity.y < 0.01 && !isGrounded) {
            isFalling = true;
        }
        else isFalling = false;
    }

    private void UpdateAnimations() {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isJumping", isJumping);
    }

    private void flip() {
        if (!isWallSliding) {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void Jump() {
        if (groundRemember > 0) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            groundRemember = -1;
            jumpRemember = 0;
            amountOfJumpsLeft--;
        } else if (!isWallSliding && !isWallJumping) {
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRemember = 0;
            amountOfJumpsLeft--;
        }
        else if ((isWallSliding || isTouchingWall)) {
            amountOfJumpsLeft = amountOfJumps;
            wallJumpTimeLeft = wallJumpTimer;
            isWallJumping = true;
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            flip();
            jumpRemember = 0;
        }
    }

    private void CheckSurroundings() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

        private void ApplyMovement() {
        if (canMove) {
            if (!isGrounded && !isWallSliding && movementDirectionInput == 0 && !isWallJumping) {
                rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
            }
            else if (!isWallJumping) {
                rb.velocity = new Vector2(movementDirectionInput * movementSpeed, rb.velocity.y);
                if (Mathf.Abs(rb.velocity.x) > movementSpeed) {
                    rb.velocity = new Vector2(movementSpeed * movementDirectionInput, rb.velocity.y);
                }
            }
            if (isGrounded) {
                amountOfJumpsLeft = amountOfJumps;
                canDoubleJump = true;
                isWallJumping = false;
                canDoubleJump = true;
                variableJumpCounter = amountOfJumps; // Variable Jump bug fix (slowing down on spamming space when falling)
            }
            if (wallJumpTimeLeft <= 0) {
                isWallJumping = false;
            }
            if (isWallSliding) {
                if (rb.velocity.y < -wallSlidingSpeed) {
                    isWallJumping = false;
                    rb.velocity = new Vector2(0, -wallSlidingSpeed);
                }
            }
        }
        else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
   

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Coin")){
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("EndCoin")) {
            Destroy(other.gameObject);
            TimeController.instance.PauseTimer();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            transform.parent = null;
        }
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadious);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}