using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private float movementDirectionInput;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool canJump;
    private bool isTouchingWall;
    private bool isWallSliding;

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


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        amountOfJumpsLeft = amountOfJumps;
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
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
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
        if (canJump == true) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
    }

    private void CheckSurroundings() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    private void ApplyMovement() {
        if (isGrounded) {
            rb.velocity = new Vector2(movementDirectionInput * movementSpeed, rb.velocity.y);
        } else if (!isGrounded && !isWallSliding && movementDirectionInput != 0){
            Vector2 forceToAdd = new Vector2(airMovementForce * movementDirectionInput, rb.velocity.y);
            rb.AddForce(forceToAdd);

            if (Mathf.Abs(rb.velocity.x) > movementSpeed) {
                rb.velocity = new Vector2(movementSpeed * movementDirectionInput, rb.velocity.y);
            }
        }
        else if (!isGrounded && !isWallSliding && movementDirectionInput == 0) {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }

        if (isWallSliding) {
            if (rb.velocity.y < -wallSlidingSpeed) {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadious);
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}