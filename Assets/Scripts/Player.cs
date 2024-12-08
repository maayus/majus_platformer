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
    public float dash_speed = 20;
    public float dash_duration = 0.2f;
    public float dash_cooldown = 1.0f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float radius = 0.2f;

    [Header("Jump Mechanics")]
    public float coyoteTime = 0.05f;
    public float jumpBufferTime = 0.2f;
    public int max_jumps = 2;

    public Health health;

    private int jumps_left = 0;

    private float jumpBufferCounter;
    private float coyoteCounter;
    private bool isGrounded;
    private Rigidbody2D rb;
    private float inputX;
    public bool is_dashing;
    public float dash_time;
    public float dash_cooldown_time;


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
            jumps_left = max_jumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump"))
        {

            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if ((coyoteCounter > 0 || jumps_left > 0) && jumpBufferCounter > 0)
        {
            jumpBufferCounter = 0;
            var jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * jump_height * rb.gravityScale);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (!isGrounded)
            {
                jumps_left--;
            }
        }

        if (Input.GetButtonDown("Fire3") && dash_cooldown_time <= 0)
        {
            is_dashing = true;
            dash_time = dash_duration;
            dash_cooldown_time = dash_cooldown;
        }

        if(is_dashing)
        {
            rb.velocity = new Vector2(dash_speed * inputX, rb.velocity.y);
            dash_time -= Time.deltaTime;

            if(dash_time <= 0)
            {
                is_dashing = false;
            }
        }
        dash_cooldown_time -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if(!is_dashing)
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
            health.TakeDamage(0.5f);
            Instantiate(bloodVfx, transform.position, Quaternion.identity);
        }
    }
}
