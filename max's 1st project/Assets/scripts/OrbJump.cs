using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbJump : MonoBehaviour
{
    public float bounceForce;

    public AnimationClip hit;

    private float hitLength;

    private Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Vector2 vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            vel.y = 0;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = vel;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce);

            anim.SetBool("Animation Done", false);
            anim.SetBool("Hit", true);
        }

    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        hitLength = hit.length;
    }

    private void Update()
    {
        if (anim.GetBool("Hit"))
        {
            StartCoroutine("Timer");
           
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(hitLength);
        anim.SetBool("Hit", false);
        anim.SetBool("Animation Done", true);
    }

}
