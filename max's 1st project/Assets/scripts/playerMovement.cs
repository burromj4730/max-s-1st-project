using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float maxSpeed = 20f;

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

    public bool canMove = true;
    public float transportPipeMoveSpeed = 200f;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetKey("left"))
            {
                rb.AddForce(Vector2.left * moveSpeed * Time.deltaTime);
            }
            else if (Input.GetKey("right"))
            {
                rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime);
            }
            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.6f, mask);
            RaycastHit2D hitright = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallmask);
            RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, wallmask);

            if (hitright.rigidbody != null || hitleft.rigidbody != null)
            {
                touchingWall = true;



                if (hitright.rigidbody != null)
                {
                    walldirection = -1;
                }
                else
                {
                    walldirection = 1;
                }

                if (touchingWall && !canWallJump)
                {
                    StartCoroutine("WallJumpTimer");
                }


            }
            else
            {
                touchingWall = false;
                walldirection = 0;
                canWallJump = false;
            }

            if (hit.rigidbody != null)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            if (Input.GetKeyDown("space"))
            {
                if (touchingWall && canWallJump)
                {

                    Vector2 jumpDir = new Vector2(1 * walldirection, wallUpForce);

                    rb.velocity = Vector2.zero;

                    rb.AddForce(jumpDir * wallJump);

                    canWallJump = false;
                }

                else if (grounded)

                {
                    rb.AddForce(Vector2.up * jump);


                }
            }
        }

    }

    private IEnumerator WallJumpTimer()
    {
        canWallJump = false;

        

        yield return new WaitForSeconds(wallJumpTimer);

        canWallJump = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            TransportPipePiece tPP = collision.gameObject.GetComponent<TransportPipePiece>();
            if (tPP.GetNextPipeDirection() == Vector2.zero)
            {
                canMove = true;
                rb.gravityScale = 2;
            }
            else
            {
                canMove = false;
                rb.gravityScale = 0;

                rb.velocity = tPP.GetNextPipeDirection() * transportPipeMoveSpeed;
            }
        }
    }
}
