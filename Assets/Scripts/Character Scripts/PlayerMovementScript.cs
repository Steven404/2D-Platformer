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

    

    [Header("For ground movement")]
    [SerializeField] public float movementSpeed;
    [SerializeField] private Rigidbody2D rb;

    [Header("For jumping")]
    [SerializeField] public float jumpForce;
    [SerializeField] public float groundCheckRadious;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask whatIsGround;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        JumpCheck();
    }

    private void FixedUpdate() {
        ApplyMovement();
        CheckSurroundings();
    }

    private void JumpCheck() {

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
        movementDirectionInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    private void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CheckSurroundings() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadious, whatIsGround);
    }

    private void ApplyMovement() {
        rb.velocity = new Vector2(movementDirectionInput * movementSpeed, rb.velocity.y);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadious);
    }
}