using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float jump = 50f;

    public float wallJump = 75f;

    public Rigidbody2D rb;

    public bool grounded;

    public LayerMask mask;

    public LayerMask wallmask;

    public int walldirection;

    public bool touchingWall;

    public bool canWallJump = false;

    public bool hasWallJumped = false;

    public float wallJumpTimer = 0.5f;

    public float wallUpForce = 1f;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left"))
        {
            rb.AddForce(Vector2.left * moveSpeed);
        }
        else if (Input.GetKey("right"))
        {
            rb.AddForce(Vector2.right * moveSpeed);
        }

        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.6f, mask);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallmask);
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, wallmask);

        if (hitright.rigidbody != null || hitleft.rigidbody != null)
        {
            touchingWall = true;
            
            
            
            if(hitright.rigidbody != null)
            {
                walldirection = -1;
            }
            else
            {
                walldirection = 1;
            }

            if (canWallJump && !hasWallJumped)
            {
                StartCoroutine("WallJumpTimer");
            }

            
        }
        else
        {
            touchingWall = false;
            walldirection = 0;
        }

        if (hit.rigidbody != null) {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKeyDown("space"))
        {
            if (touchingWall&&canWallJump)
            {

                Vector2 jumpDir = new Vector2(1 * walldirection, wallUpForce);

                rb.velocity = Vector2.zero;

                rb.AddForce(jumpDir * wallJump);

                hasWallJumped = true;
            }

            else if (grounded)
            
            {
                rb.AddForce(Vector2.up * jump);

                hasWallJumped = false;
            }
        }

    }

    private IEnumerator WallJumpTimer()
    {
        canWallJump = false;

        hasWallJumped = false;

        yield return new WaitForSeconds(wallJumpTimer);

        canWallJump = true;

    }
}
