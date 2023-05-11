using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbJump : MonoBehaviour
{
    public float bounceForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            vel.y = 0;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = vel;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce);
        }

       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            vel.y = 0;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = vel;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce);
        }

    }
}
