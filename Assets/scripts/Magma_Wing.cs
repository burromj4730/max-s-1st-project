using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma_Wing : MonoBehaviour
{
    
    public enum state
    {
        IDLE = 0, ALERTED, AGRO
    }
    [Header("FSM")]
    public state currentState;

    public Animator magmaWing;

    public float idleRadius;

    public LayerMask playerMask;

    public bool drawGizmos;

    private bool playerInRadius;

    public float timeBetweenAtacks;

    public float freezePosMomento;

    [Header("References, Movement & shooting")]
    public float moveSpeed;
    private Transform player;

    private Rigidbody2D RB;


    public float idealDistance;

    public float acceleration;

    public float fireBallMovespeed;
    public GameObject fireBall;
    public Transform fireSpawn;

    public float attackEndFrames;

    public bool alertStarted = false;

    public ParticleSystem shootParticle;

   [Header("Debug Booleans")]
    [SerializeField] private bool attackStarted;

    [SerializeField] private bool attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkConditions();
        runState();
        if (playerInRadius)
        {
            if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
    private void runState()
    {
        switch (currentState)
        {
            case state.IDLE:
                magmaWing.SetBool("Idle", true);
                magmaWing.SetBool("Alerted", false);
                magmaWing.SetBool("Agro", false);
                if (playerInRadius)
                {
                    Follow();
                }
                break;
            case state.ALERTED:
                magmaWing.SetBool("Idle", false);
                magmaWing.SetBool("Alerted", true);
                magmaWing.SetBool("Agro", false);
                if (!alertStarted)
                {
                    Invoke("alertTimer", 0.5f);
                }
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
                if (!playerInRadius)
                {
                    RaycastHit2D[] circle = Physics2D.CircleCastAll(transform.position, idleRadius, Vector2.up, 0f, playerMask);
                    if (circle.Length!=0)
                    {
                        currentState = state.ALERTED;
                        playerInRadius = true;
                        player = circle[0].transform;
                    }
                }
                else
                {
                    if (!attackCooldown && !attackStarted)
                    {
                        currentState = state.AGRO;
                    }
                }
                break;
            case state.AGRO:
                Attack();
                break;
        }
    }
    void alertTimer()
    {
        currentState = state.IDLE;
    }
    void Attack()
    {
        if (!attackStarted)
        {
            attackStarted = true;
            Follow();
            Invoke("AttackFreese", freezePosMomento);
        }
    }
    void AttackFreese()
    {
        RB.velocity = Vector2.zero;
        Vector3 direction = (player.position - fireSpawn.position).normalized;
        GameObject newFire = Instantiate(fireBall, fireSpawn.position, Quaternion.identity);
        newFire.transform.up = -direction;
        newFire.GetComponent<Rigidbody2D>().velocity = direction * fireBallMovespeed;
        shootParticle.Play();
        Invoke("FinishAttack", attackEndFrames);
        Follow();
    }
    void FinishAttack()
    {
        currentState = state.IDLE;
        attackCooldown = true;
        attackStarted = false;
        Invoke("AttackCooldown", timeBetweenAtacks);
    }
    void AttackCooldown()
    {
        attackCooldown = false;
    }
    void Follow()
    {
        Vector3 direction = player.position - transform.position;
        direction = direction.normalized;
        if (Vector2.Distance(player.position, transform.position) < idealDistance)  
        {
            if (RB.velocity.magnitude < ((Vector2)direction * -moveSpeed).magnitude)
            {
                RB.velocity += (Vector2)direction * -acceleration;
            }
            else
            {
                RB.velocity = (Vector2)direction * -moveSpeed;
            }
            
        }
        else
        {
            if (RB.velocity.magnitude < ((Vector2)direction * moveSpeed).magnitude)
            {
                RB.velocity += (Vector2)direction * acceleration;
            }
            else
            {
                RB.velocity = (Vector2)direction * moveSpeed;
            }
        }
       
    }
}
