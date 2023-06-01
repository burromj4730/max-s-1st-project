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

    public Animator animator;

    private bool sleepTime;

    public AnimationClip wakeUp;

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
                animator.SetBool("Moving", true);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey("right"))
            {
                rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime);
                animator.SetBool("Moving", true);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                animator.SetBool("Moving", false);
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
                animator.SetBool("Jumping", false);
                animator.SetBool("Falling", false);
            }
            else
            {
                grounded = false;
                if (rb.velocity.y < 0)
                {
                    animator.SetBool("Falling", true);
                }
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
                    animator.SetBool("Jumping", true);
                    rb.AddForce(Vector2.up * jump);



                }
            }
        }
        if (!Input.anyKey && !sleepTime && !(animator.GetBool("Sleeping") && animator.GetBool("Moving") && animator.GetBool("Falling") && animator.GetBool("Jumping") && animator.GetBool("WAKE UP")))
        {
            StartCoroutine(SleepTimer());
            Debug.Log("Timer is sterted");
        }
        else if (Input.anyKey && (animator.GetBool("Sleeping") || sleepTime))
        {
            StopCoroutine(SleepTimer());
            sleepTime = false;
            animator.SetBool("Sleeping", false);
            animator.SetBool("WAKE UP", true);
            StartCoroutine(WakeUpTimer());
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
    IEnumerator SleepTimer()
    {
        sleepTime = true;
        yield return new WaitForSeconds(5);
        Debug.Log("Animation Playing");
        sleepTime = false;
        animator.SetBool("Sleeping", true);
    }
    IEnumerator WakeUpTimer()
    {
        yield return new WaitForSeconds(wakeUp.length);
        animator.SetBool("WAKE UP", false);
    }
}
