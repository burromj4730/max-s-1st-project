using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRetreatEnemy : MonoBehaviour
{
    [Header("Variables")]

    public float seePlayerRadius;
    public LayerMask layerMask;

    private Vector3 startingPosition;

    [SerializeField] private int ticksPerSecond;
    private float secondsBetweenTicks;
    private bool startNextTick = true;

    [Header("StateManagement")]

    [SerializeField] private State m_state;

    [System.Serializable]private enum State
    {
        IDLE = 0,
        CHASE,
        RETREAT
    }

    [Header("Idle Variables")]
    public Color seePlayerGizmoColour;
    [SerializeField][Tooltip("Chance out of 100(%) for the enemy to move in idle tick")] private int chanceOfMove;

    [Header("Chase Variables")]
    public Color chaseDistanceGizmoColour;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float distanceAbleToChase;
    [SerializeField] private float chaseSpeed = 5f;

    [Header("Retreat Variables")]
    [SerializeField] private float reachedRetreatPositionOffset = 0.1f;

    [Header("Debugging")]
    [SerializeField] private bool drawDebugGizmos = false;

    private State StateCheck()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, seePlayerRadius, Vector2.up, 1, layerMask);

        if (m_state == State.IDLE)
        {
            if (hits.Length > 0)
            {
                targetObject = hits[0].collider.gameObject;
                return State.CHASE;
            }
        }
        else if (m_state == State.CHASE)
        {
            if (hits.Length > 0)
            {
                targetObject = hits[0].collider.gameObject;
                return State.CHASE;
            }
            else
            {
                return State.RETREAT;
            }
        }
        else
        {
            if (Mathf.Abs(Vector2.Distance(transform.position, startingPosition)) > reachedRetreatPositionOffset)
            {
                return State.RETREAT;
            }
        }
        return 0;
    }

    private void RunState()
    {
        switch (m_state)
        {
            case State.IDLE:

                break;

            case State.CHASE:

                break;

            case State.RETREAT:
                
                break;
        }
    }

    private void Start()
    {
        startingPosition = transform.position;
        secondsBetweenTicks = 1f / ticksPerSecond;
    }

    private void Update()
    {
        if (startNextTick)
        {
            m_state = StateCheck();

            RunState();

            startNextTick = false;
            StartCoroutine(WaitForNextTick());
            Debug.Log("TICK");
        }
    }

    private IEnumerator WaitForNextTick()
    {
        yield return new WaitForSecondsRealtime(secondsBetweenTicks);
        startNextTick = true;
    }

    private void OnDrawGizmos()
    {
        if (drawDebugGizmos)
        {
            switch(m_state)
            {
                case State.IDLE:
                    Gizmos.color = seePlayerGizmoColour;
                    Gizmos.DrawWireSphere(transform.position, seePlayerRadius);
                    break;
                case State.CHASE:
                    Gizmos.color = chaseDistanceGizmoColour;
                    Gizmos.DrawWireSphere(startingPosition, distanceAbleToChase);
                    break;
                case State.RETREAT:
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(transform.position, seePlayerRadius);
                    break;
            }
        }
    }
}
