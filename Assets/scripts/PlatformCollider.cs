using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    public Collider2D M_collider;

    public Rigidbody2D player;

    [Range(0f, 5f)] public float floating_Movement;

    [Range(0f, 0.1f)] public float floating_Speed;

    private bool moving_Up = false;

    private float starting_Y;
    // Start is called before the first frame update
    void Start()
    {
        starting_Y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (moving_Up)
        {
            if (transform.position.y < starting_Y + floating_Movement)
            {
                transform.position += new Vector3(0, floating_Speed, 0);
            }
            else
            {
                moving_Up = false;
            }
        }
        else
        {
            if (transform.position.y > starting_Y - floating_Movement)
            {
                transform.position -= new Vector3(0, floating_Speed, 0);
            }
            else
            {
                moving_Up = true;
            }
        }
                if (player.velocity.y > 0)
        {
            M_collider.enabled = false;
        }
        else
        {
            M_collider.enabled = true;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.contacts[0].point.y > player.transform.position.y)
    //    {
    //        M_collider.enabled = false;
    //    }
    //    else
    //    {
    //        M_collider.enabled = true;
    //    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    M_collider.enabled = true;
    //}
}
