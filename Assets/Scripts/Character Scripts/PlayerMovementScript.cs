using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private float movementDirectionInput;

    private bool isFacingRight = true;
    private int facingDirection = 1;
    private bool isWalking;
    private bool isGrounded;
    private bool canJump;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isWallJumping;
  
    private int amountOfJumpsLeft;

    

    [Header("For ground movement")]
    [SerializeField] public float movementSpeed;
    [SerializeField] private Rigidbody2D rb;

    [Header("For Air Movement")]
    [SerializeField] public float airMovementForce;
    [SerializeField] public float airDragMultiplier;

    [Header("For jumping")]
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


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        JumpCheck();
        CheckIfWallSliding();
    }

    private void FixedUpdate() {
        ApplyMovement();
        CheckSurroundings();
        Vector3 whereIAm = Camera.main.transform.position;
        Vector3 whereIShouldBe = transform.position - new Vector3(0, 0, 10);
        Camera.main.transform.position = Vector3.Lerp(whereIAm, whereIShouldBe, 0.085f);
    }

    public void LateUpdate() {
        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
    }


    private void CheckIfWallSliding() {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0) {
            isWallSliding = true;
        } else {
            isWallSliding = false;
        }
    }

    private void JumpCheck() {
        if (isGrounded && rb.velocity.y <= 0.0001f) {
            amountOfJumpsLeft = amountOfJumps;
        }
        if (amountOfJumpsLeft <=0) {
            canJump = false;
        } else {
            canJump = true;
        }
    }

    private void CheckMovementDirection() {
        if(isFacingRight && movementDirectionInput < 0) {
            flip();
        } else if(!isFacingRight && movementDirectionInput > 0) {
            flip();
        }
    }

    private void flip() {
        if (!isWallSliding) {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        
    }

    private void CheckInput() {
        movementDirectionInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
        if(Input.GetButtonUp("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }

    private void Jump() {
        if (canJump && !isWallSliding) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        } else if (isWallSliding && Input.GetButtonDown("Fire3")) {
            isWallJumping = true;
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x *-facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            flip();
        } else if ((isWallSliding || isTouchingWall) && Input.GetButtonDown("Jump")) {
            isWallJumping = true;
            isWallSliding = false;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * -facingDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            flip();
        }
    }

    private void CheckSurroundings() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void ApplyMovement() {
        if (isGrounded) {
            isWallJumping = false;
            rb.velocity = new Vector2(movementDirectionInput * movementSpeed, rb.velocity.y);
        } else if (!isGrounded && !isWallSliding && movementDirectionInput != 0 && !isWallJumping){
            Vector2 forceToAdd = new Vector2(airMovementForce * movementDirectionInput, rb.velocity.y);
            rb.AddForce(forceToAdd);

            if (Mathf.Abs(rb.velocity.x) > movementSpeed) {
                rb.velocity = new Vector2(movementSpeed * movementDirectionInput, rb.velocity.y);
            }
        }
        else if (!isGrounded && !isWallSliding && movementDirectionInput == 0 && !isWallJumping) {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
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