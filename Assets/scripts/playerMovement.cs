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

    public bool sleepTimerStarted;

    public float AFKTime;

    public bool lockMovement;

    public GameObject Zee;

    private bool idleTimerStarted;

    private bool idleTime;

    public float sleepTimer;

    public int idleAnimCount;

    public bool facingRight;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!lockMovement)
        {
            Movement();
        }

        if (!Input.anyKey && !Input.anyKeyDown && !sleepTimerStarted && !sleepTime && !idleTimerStarted)
        {
            StartCoroutine("SleepTimer");
            StartCoroutine("IdleTimer");
            idleTimerStarted = true;
            sleepTimerStarted = true;
        }
        if (Input.anyKey||Input.anyKeyDown)
        {
            StopCoroutine("SleepTimer");
            StopCoroutine("ZTimer");
            StopCoroutine("IdleTimer");
            idleTimerStarted = false;
            sleepTimerStarted = false;
            if (sleepTime)
            {
                sleepTime = false;
                animator.SetBool("Sleeping", false);
                animator.SetBool("WAKE UP", true);
                grounded = false;
                StartCoroutine(WakeUpTimer());
            }
            else if (idleTime)
            {
                for (int i = 1; i <= idleAnimCount; i++)
                {
                    animator.SetBool("Idle" + i.ToString(), false);
                }
                idleTime = false;
            }
        }
        int dir = GetComponent<SpriteRenderer>().flipX ? -1 : 1;
        RaycastHit2D edgeCheck = Physics2D.Raycast(transform.position + (new Vector3(0.3f, 0) * dir), -Vector2.up, 0.6f, mask);
        if (edgeCheck.rigidbody == null)
        {
            animator.SetBool("Looking over Edge", true);
        }
        else
        {
            animator.SetBool("Looking over Edge", false);
        }
    }

    private void Movement()
    {
        if (canMove)
        {
            if (Input.GetKey("left"))
            {
                rb.AddForce(Vector2.left * moveSpeed * Time.deltaTime);
                animator.SetBool("Moving", true);
                GetComponent<SpriteRenderer>().flipX = true;

                for (int i = 1; i <= idleAnimCount; i++)
                {
                    animator.SetBool("Idle" + i.ToString(), false);
                }
                idleTime = false;
                facingRight = false;
            }
            else if (Input.GetKey("right"))
            {
                rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime);
                animator.SetBool("Moving", true);
                GetComponent<SpriteRenderer>().flipX = false;

                for (int i = 1; i <= idleAnimCount; i++)
                {
                    animator.SetBool("Idle" + i.ToString(), false);
                }
                idleTime = false;
                facingRight = true;
            }
            else if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                animator.SetBool("Moving", false);
            }
            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
            else if (rb.velocity.x < -maxSpeed)
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
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
        else if (collision.gameObject.layer == 3)
        {
            animator.SetBool("Falling (to our death)", true);
            transform.GetChild(0).transform.parent = null;
            Spiked s = FindObjectOfType<Spiked>();
            s.StartCoroutine(s.ExplodePlayer(this.gameObject));
            s.StartCoroutine(s.reloadScene());
        }
    }
    IEnumerator SleepTimer()
    {
        yield return new WaitForSeconds(sleepTimer);
        sleepTimerStarted = false;
        sleepTime = true;
        animator.SetBool("Sleeping", true);
        StartCoroutine("ZTimer");
        lockMovement = true;
    }

    IEnumerator IdleTimer()
    {
        Debug.Log("IdleTimerTriggered");
        yield return new WaitForSeconds(AFKTime);
        idleTimerStarted = false;
        idleTime = true;
        int random = Random.Range(1, idleAnimCount + 1);
        animator.SetBool("Idle"+random.ToString(), true);
        StartCoroutine("TurnOffCurrentIdle");
    }

    IEnumerator TurnOffCurrentIdle()
    {
        yield return new WaitForSeconds(0.1f);
        AnimatorClipInfo[] info = animator.GetCurrentAnimatorClipInfo(0);
        yield return new WaitForSeconds(info[0].clip.length);
        for (int i = 1; i <= idleAnimCount; i++)
        {
            animator.SetBool("Idle" + i.ToString(), false);
        }
        idleTime = false;
        idleTimerStarted = true;
        StartCoroutine("IdleTimer");
    }
    IEnumerator WakeUpTimer()
    {
        Zee.SetActive(false);
        yield return new WaitForSeconds(wakeUp.length);
        animator.SetBool("WAKE UP", false);
        lockMovement = false;
    }

    IEnumerator ZTimer()
    {
        yield return new WaitForSeconds(1.5f);
        Zee.SetActive(true);
    }
    public void FirePowerUp()
    {
        animator.SetLayerWeight(1, 1f);
        animator.SetBool("PowerUp Fire", true);
        lockMovement = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        StartCoroutine(FireTimer());
    }
    IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("PowerUp Fire", false);
        lockMovement = false;
        rb.gravityScale = 2f;
    }
}
