using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    public float startPos;

    public float bottomPos;

    public float moveUpspeed;

    public float downGravityScale;

    public float upTimer;

    public float downTimer;

    public bool isUp;

    public bool isDown;

    public bool fallingDown;

    public bool timerStarted;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerStarted)
        {
            if (isUp || isDown)
            {
                if (isUp)
                {
                    StartCoroutine("UpTimer");
                }
                else
                {
                    StartCoroutine("DownTimer");
                }
                timerStarted = true;
            }
            if (fallingDown && transform.position.y > bottomPos)
            {
                //Scale up RB gravity scale then check still above bottom pos
            }

        }
    }
}
