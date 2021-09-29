using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private float movementDirectionInput;

    private int variableJumpCounter;


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

    private int facingDirection = 1;

    private float wallJumpTimeLeft;
    //private float dashTimeLeft;
    //private float lastDash;

    private Animator animator;

    private int amountOfJumpsLeft;

    private float jumpRemember;

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
    [SerializeField] public float jumpRememberTime = 0.35f;
    [SerializeField] public float jumpForce;
    [SerializeField] public int amountOfJumps;
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
    [SerializeField] public float wallJumpTimer = 1.5f; // allowing the player to controller the player after a specified time


    void Start() {
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
        //CheckDash();
    }

    private void FixedUpdate() {
        ApplyMovement();
        CheckSurroundings();
    }

    public void LateUpdate() {
        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
    }

    private void CheckInput() {
        movementDirectionInput = Input.GetAxisRaw("Horizontal");
        jumpRemember -= Time.deltaTime;
        wallJumpTimeLeft -= Time.deltaTime;

        if (Input.GetButtonDown("Jump")) {
            jumpRemember = jumpRememberTime;
        }
        if ((jumpRemember > 0 && isGrounded) || ((isWallSliding) && jumpRemember > 0) || (jumpRemember > 0 && !isTouchingWall && canDoubleJump == true)) {
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
        if(isFacingRight && movementDirectionInput < 0 && !isWallJumping) {
            flip();
        } else if(!isFacingRight && movementDirectionInput > 0 && !isWallJumping) {
            flip();
        }
        if(isGrounded && rb.velocity.y <= 0.001f && movementDirectionInput !=0) {
            isRunning = true;
        } 
        else {
            isRunning = false;
        }
        if (rb.velocity.y > 0.1) {
            isJumping = true;
        }
        else isJumping = false;
        if (rb.velocity.y < 0.1 && !isGrounded) {
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
        if (isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRemember = 0;
            canDoubleJump = true;
        } else if (!isWallSliding && !isWallJumping) {
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRemember = 0;
        }
        else if ((isWallSliding || isTouchingWall)) {
            canDoubleJump = true;
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
        if (isGrounded && rb.velocity.y <= 0.001f) {
            isWallJumping = false;
            canDoubleJump = true;
            variableJumpCounter = amountOfJumps; // Variable Jump bug fix (slowing down on spamming space when falling)
            amountOfJumpsLeft = amountOfJumps;
            rb.velocity = new Vector2(movementDirectionInput * movementSpeed, rb.velocity.y);
        } else if (!isGrounded && !isWallSliding && movementDirectionInput != 0 && !isWallJumping){
            Vector2 forceToAdd = new Vector2(airMovementForce * movementDirectionInput, rb.velocity.y);
            rb.AddForce(forceToAdd);
            if (Mathf.Abs(rb.velocity.x) > movementSpeed) {
                rb.velocity = new Vector2(movementSpeed * movementDirectionInput, rb.velocity.y);
            }
        } else if (!isGrounded && !isWallSliding && movementDirectionInput == 0 && !isWallJumping) {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
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

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadious);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}