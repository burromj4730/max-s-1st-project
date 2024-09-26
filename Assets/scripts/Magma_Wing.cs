using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma_Wing : MonoBehaviour
{
    public enum state
    {
        IDLE = 0, ALERTED, AGRO
    }
    public state currentState;

    public Animator magmaWing;

    public float idleRadius;

    public LayerMask playerMask;

    public bool drawGizmos;

    private bool playerInRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkConditions();
        runState();
    }
    private void runState()
    {
        switch (currentState)
        {
            case state.IDLE:
                magmaWing.SetBool("Idle", true);
                magmaWing.SetBool("Alerted", false);
                magmaWing.SetBool("Agro", false);
                break;
            case state.ALERTED:
                magmaWing.SetBool("Idle", false);
                magmaWing.SetBool("Alerted", true);
                magmaWing.SetBool("Agro", false);
                Invoke("alertTimer", 0.5f);
                break;
            case state.AGRO:
                magmaWing.SetBool("Idle", false);
                magmaWing.SetBool("Alerted", false);
                magmaWing.SetBool("Agro", true);
                break;
            default:
                Debug.Log("UNKNOWN STATE - MAGMA WING");
                break;
        }
    }
    private void checkConditions()
    {
        switch (currentState)
        {
            case state.IDLE:
                RaycastHit2D[] circle = Physics2D.CircleCastAll(transform.position, idleRadius, Vector2.up, 0f, playerMask);

                if (circle.Length != 0)
                {
                    if (playerInRadius == false)
                    {
                        currentState = state.ALERTED;
                        playerInRadius = true;
                    }
                    
                }
                else
                {
                    playerInRadius = false;
                }
                break;
        }
    }
    void alertTimer()
    {
        currentState = state.IDLE;
    }
}
