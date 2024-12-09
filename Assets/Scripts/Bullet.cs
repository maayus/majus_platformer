using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impact;
    public Vector3 direction = Vector3.right;
    public float speed = 20;
    public Vector2 damage_range = new Vector2(10, 20);
    public float lifetime = 3;

    private Rigidbody2D rb;


    void Start()
    {
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var damage = Random.Range(damage_range.x, damage_range.y);

        var health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage((int)damage);
        }

        Instantiate(impact, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
