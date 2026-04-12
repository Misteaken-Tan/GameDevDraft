using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerControls : MonoBehaviour
{

    //Declare Rigidbody 2D
    Rigidbody2D rb;

    //Declare Movement Multiplier
    public float moveSpeed = 10f;

    //Declare Jump Force
    public float jumpForce = 10f;

    //Check if Player is on Ground
    public bool isGrounded = true;

    float hAxis;
    public bool mobileControls;

    //Gravity
    public float BaseGravity = 2;
    public float MaxFallSpeed = 18f;
    public float FallMultiplier = 2f;

    void Start()
    {
        //Get Rigidbody 2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        //Store Horizontal Axis
       if(!mobileControls) hAxis = Input.GetAxis("Horizontal");

        //Set Velocity
        rb.linearVelocity = new Vector2(hAxis * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        Gravity();


    }

    public void jump()
    {
        // We check isGrounded here so the button only works when on the floor
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // Prevents double jumping immediately
        }
    }

    public void moveLeft()
    {
        hAxis = -1; // Move left
    }
    public void moveRight()
    {
       hAxis = 1; // Move right
    }

    public void stopMoving()
    {   
        hAxis = 0; // Stop horizontal movement
    }

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

    private void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = BaseGravity * FallMultiplier; ;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -MaxFallSpeed));
        }
        else
        {
            rb.gravityScale = BaseGravity;
        }
    }
}
