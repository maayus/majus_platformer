using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("VFX")]
    public GameObject bloodVfx;

    [Header("Movement")]
    public float movement_speed = 10.0f;
    public float jump_height = 3;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float radius = 0.2f;

    [Header("Jump Mechanics")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    private float jumpBufferCounter;
    private float coyoteCounter;
    private bool isGrounded;
    private bool doubleJump;
    private Rigidbody2D rb;
    private float inputX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
            doubleJump = true;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump"))
        {
            if (doubleJump && !isGrounded && coyoteCounter <= 0)
            {
                doubleJump = false;
                var jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * jump_height * rb.gravityScale);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else
            {
                jumpBufferCounter = jumpBufferTime;
            }
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteCounter > 0 && jumpBufferCounter > 0)
        {
            jumpBufferCounter = 0;
            var jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * jump_height * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * movement_speed, rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (groundCheck != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, radius);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.relativeVelocity.magnitude > 25)
        {
            Instantiate(bloodVfx, transform.position, Quaternion.identity);
        }
    }
}
