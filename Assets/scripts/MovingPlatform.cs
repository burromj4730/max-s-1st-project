using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool movingUp = true;

    public float topPos;

    public float botPos;

    public float moveSpeed;

    public float timer;

    public bool useTimer;

    bool frozen;
    // Start is called before the first frame update
    void Start()
    {
        if (movingUp)
        {
            topPos = transform.position.y + topPos;

            botPos = transform.position.y;
            
        }
        else
        {
            topPos = transform.position.y;

            botPos = transform.position.y - botPos;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp && transform.position.y < topPos)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime);
        }
        else if (movingUp&&transform.position.y > topPos)
        {
           if (useTimer)
            {
                if (!frozen)
                {
                    frozen = true;
                    StartCoroutine("WaitTimer");
                }

            }
            else
            {
                movingUp = false;
            }
        }
        else if (!movingUp&&transform.position.y > botPos)
        {
            transform.position -= new Vector3(0, moveSpeed * Time.deltaTime);
        }
        else if (!movingUp && transform.position.y < botPos)
        {
            if (useTimer)
            {
                if (!frozen)
                {
                    frozen = true;
                    StartCoroutine("WaitTimer");
                }

            }
            else
            {
                movingUp = true;
            }
        }
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(timer);

        movingUp = !movingUp;
        frozen = false;
    }
}
