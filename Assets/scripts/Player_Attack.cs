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
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    else
                    {
                        if (hit.transform.position.x < transform.position.x)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
            animator.SetBool("Attack", true);
            StartCoroutine(AttackCoolDown());
        }
    }
    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
        animator.SetBool("Attack", false);
    }
}
