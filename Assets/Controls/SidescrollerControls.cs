using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerControls : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 12f;
    public bool mobileControls = true; // Set this to TRUE in Inspector for buttons

    [Header("Physics Settings")]
    public float BaseGravity = 2f;
    public float MaxFallSpeed = 18f;
    public float FallMultiplier = 2.5f;

    [Header("Status (Read Only)")]
    public bool isGrounded = true;

    // References
    Rigidbody2D rb;
    Animator anim;
    float hAxis;
    bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. INPUT HANDLING
        if (!mobileControls)
        {
            // Keyboard Controls
            hAxis = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                jump();
            }
        }
        // If mobileControls is true, hAxis is set via moveLeft/Right functions below

        // 2. APPLY MOVEMENT
        rb.linearVelocity = new Vector2(hAxis * moveSpeed, rb.linearVelocity.y);

        // 3. APPLY CUSTOM PHYSICS & ANIMATIONS
        Gravity();
        UpdateAnimations();
        FlipCheck();
    }

    public void moveLeft()
    {
        Debug.Log("UI: Moving Left");
        hAxis = -1;
    }

    public void moveRight()
    {
        Debug.Log("UI: Moving Right");
        hAxis = 1;
    }

    public void stopMoving()
    {
        Debug.Log("UI: Stopped Moving");
        hAxis = 0;
    }

    public void jump()
    {
        // This is the "Gatekeeper" that prevents infinite jumping
        
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // Prevents double jump until trigger hits ground again
            SoundManager.instance.PlaySFX(SoundManager.instance.jumpSound);
        }
    }

    // --- CORE LOGIC ---

    private void UpdateAnimations()
    {
        if (anim != null)
        {
            // Matches your Blend Tree and Parameters
            anim.SetFloat("xVelocity", Mathf.Abs(hAxis));
            anim.SetFloat("yVelocity", rb.linearVelocity.y);
            anim.SetBool("isJumping", !isGrounded);
        }
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

    private void Gravity()
    {
        // Custom gravity logic for a "snappy" feel
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

    // --- GROUND DETECTION ---
    // Make sure your platforms are on Layer 3
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }
}