using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movement_speed = 10.0f;
    public float jump_height = 3;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float radius = 0.2f;

    private bool isGrounded;
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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            var jumpForce = Mathf.Sqrt(-2 * Physics2D.gravity.y * jump_height);
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
}
