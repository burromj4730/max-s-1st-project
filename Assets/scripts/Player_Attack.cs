using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private playerMovement movment;

    public KeyCode attack;

    private bool canAttack;

    public float attackCoolDown;

    public float attackRange;

    public LayerMask hitRadius;

    private Animator animator;

    public GameObject trail;

    public GameObject magmaDeath;

    // Start is called before the first frame update
    void Start()
    {
        movment = GetComponent<playerMovement>();
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attack))
        {
            Attack();
        }
    }
    void Attack()
    {
        if (canAttack)

        {
            canAttack = false;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.up, 1f, hitRadius);
            if(hits.Length != 0)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    if (movment.facingRight)
                    {
                        if (hit.transform.position.x > transform.position.x)
                        {
                            GameObject particals = Instantiate(magmaDeath, hit.transform.position, Quaternion.identity);
                            particals.transform.position = hit.transform.position;
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    else
                    {
                        
                        if (hit.transform.position.x < transform.position.x)
                        {
                            GameObject particals = Instantiate(magmaDeath, hit.transform.position, Quaternion.identity);
                            particals.transform.position = hit.transform.position;
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
            if (movment.facingRight)
            {
                animator.SetBool("Attack Right", true);
            }
            else
            {
                animator.SetBool("Attack Left", true);
            }
            StartCoroutine(AttackCoolDown());
        }
    }
    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
        animator.SetBool("Attack Right", false);
        animator.SetBool("Attack Left", false);
    }
    
}
