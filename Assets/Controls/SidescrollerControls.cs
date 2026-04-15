using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerControls : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim; // 1. Reference the Animator

    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public bool isGrounded = true;

    float hAxis;
    public bool mobileControls;
    bool facingRight = true; // For flipping the sprite

    public float BaseGravity = 2;
    public float MaxFallSpeed = 18f;
    public float FallMultiplier = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // 2. Initialize Animator
    }

    void Update()
    {
        if (!mobileControls) hAxis = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(hAxis * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
            JumpLogic();

        Gravity();

        // 3. Update Animator Parameters
        UpdateAnimations();

        // 4. Flip the character sprite
        FlipCheck();
    }

    private void UpdateAnimations()
    {
        // Set 'Speed' to the absolute value of hAxis (0 to 1)
        // Use Mathf.Abs so that -1 (left) still triggers the run animation
        anim.SetFloat("xVelocity", Mathf.Abs(hAxis));

        // Set 'isGrounded' so the animator knows if we are in the air
        anim.SetBool("isJumping", !isGrounded);

        // Optional: If you have a 'VerticalSpeed' parameter for falling vs jumping
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void FlipCheck()
    {
        if (hAxis > 0 && !facingRight) Flip();
        else if (hAxis < 0 && facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void JumpLogic()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        // We set isGrounded to false immediately for snappier animation transitions
        isGrounded = false;
    }

    public void jump() => JumpLogic();

    public void moveLeft() => hAxis = -1;
    public void moveRight() => hAxis = 1;
    public void stopMoving() => hAxis = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) isGrounded = false;
    }

    private void Gravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = BaseGravity * FallMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -MaxFallSpeed));
        }
        else
        {
            rb.gravityScale = BaseGravity;
        }
    }
}