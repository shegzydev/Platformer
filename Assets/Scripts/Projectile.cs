using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] GameObject impact;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(impact, collision.GetContact(0).point, Quaternion.identity);

        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null) damageable.TakeDamage();

        Destroy(gameObject);
    }
}
