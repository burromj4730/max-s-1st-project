using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    private float startPos;

    private float bottomPos;

    public float bottomOffset = 3.35f;

    public float moveUpspeed;
    
    [Range(0.001f, 1f)] public float downGravityScale;

    public float upTimer;

    public float downTimer;

    public bool isUp;

    public bool isDown;

    public bool fallingDown;

    public bool timerStarted;

    public Rigidbody2D RB;

    public float startDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.y;
        bottomPos = startPos - bottomOffset;
        fallingDown = true;

        StartCoroutine("StartDelay");
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerStarted)
        {
            if (fallingDown && transform.position.y > bottomPos)
            {
                RB.gravityScale += downGravityScale;
            }
            else if (fallingDown && transform.position.y <= bottomPos)
            {
                RB.gravityScale = 0;
                RB.velocity = Vector2.zero;
                transform.position = new Vector3(transform.position.x, bottomPos, transform.position.z);
                fallingDown = false;

                StartCoroutine("DownTimer");

            }
            else if (transform.position.y >= startPos)
            {
                RB.velocity = Vector2.zero;
                transform.position = new Vector3(transform.position.x, startPos, transform.position.z);

                StartCoroutine("UpTimer");
            }
            else
            {
                
                RB.velocity = new Vector2(0, moveUpspeed);
            }
        }
    }

    private IEnumerator UpTimer()
    {
        timerStarted = true;
        yield return new WaitForSeconds(upTimer);
        
        fallingDown = true;
        RB.gravityScale = 1;

        timerStarted = false;
    }
    private IEnumerator DownTimer()
    {
        timerStarted = true;
        yield return new WaitForSeconds(downTimer);
        
        timerStarted = false;
    }

    private IEnumerator StartDelay()
    {
        timerStarted = true;
        yield return new WaitForSeconds(startDelay);

        timerStarted = false;
    }
}
