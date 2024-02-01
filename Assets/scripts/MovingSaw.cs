using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D RB;

    public Transform[] positions;
    
    public float reachedTargetTolerance = 0.2f;

    private bool movingForward = true;

    private int currentTarget;

    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = positions[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(positions[currentTarget].position, transform.position) <= reachedTargetTolerance)
        {
            if (movingForward)
            {
                if (currentTarget == positions.Length - 1)
                {
                    movingForward = false;
                    currentTarget -= 1;
                }
                else
                {
                    currentTarget += 1;
                }
            }
            else
            {
                if (currentTarget == 0)
                {
                    movingForward = true;
                    currentTarget += 1;
                }
                else
                {
                    currentTarget -= 1;
                }
            }

            Vector2 moveDir = positions[currentTarget].position - transform.position;
            velocity = moveDir.normalized * moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (RB.velocity != velocity)
        {
            RB.velocity = velocity;
        }
    }
}
