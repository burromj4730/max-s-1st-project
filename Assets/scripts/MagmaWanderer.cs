using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaWanderer : MonoBehaviour
{
    public Rigidbody2D RB;

    public float direction;

    private float moveSpeed;

    public LayerMask mask;

    private bool agro;

    public float noticeRadius;

    public LayerMask playerMask;

    public float agroSpeed;

    public float wanderSpeed;

    public Animator magmaWanderer;

    private bool canMove = true;

    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D circle = Physics2D.Raycast(transform.position, new Vector2(direction, 0), noticeRadius, playerMask);

        if(circle.collider != null)
        {
            agro = true;
            magmaWanderer.SetBool("Attack", true);
            particles.SetActive(true);
        }
        else
        {
            agro = false;
            magmaWanderer.SetBool("Attack", false);
            particles.SetActive(false);
            if (direction == -1)
            {
                magmaWanderer.Play("Magma Wander", 0, 1);
            }
        }

        if (!agro)
        {
            Wander();
            moveSpeed = wanderSpeed;
        }
        else
        {
            Agro(circle.collider.gameObject);
            moveSpeed = agroSpeed;
        }
        if (canMove)
        {
            RB.velocity = new Vector2(direction * moveSpeed, 0);
        }
        else
        {
            RB.velocity = Vector2.zero;
        }

    }
    private void Wander()
    {
        RaycastHit2D hitleftDown = Physics2D.Raycast(transform.position - new Vector3(0.5f, 0), -Vector2.up, 0.6f, mask);
        RaycastHit2D hitrightDown = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0), -Vector2.up, 0.6f, mask);
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, mask);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, mask);
        if ((hitleftDown.collider == null && direction == -1) || (hitleft.collider != null && direction == -1))
        {
            direction = 1;
            magmaWanderer.SetBool("Turn Left", false);
            magmaWanderer.SetBool("Turn Right", true);
            StartCoroutine(WaitForAnimation());
            particles.transform.rotation = Quaternion.Euler(-110, 90, -90);
        }
        if ((hitrightDown.collider == null && direction == 1) || (hitright.collider != null && direction == 1))
        {
            direction = -1;
            magmaWanderer.SetBool("Turn Right", false);
            magmaWanderer.SetBool("Turn Left", true);
            StartCoroutine(WaitForAnimation());
            particles.transform.rotation = Quaternion.Euler(-70, 90, -90);
        }
        
    }

    private void Agro(GameObject Player)
    {
       
    }

    IEnumerator WaitForAnimation()
    {
        canMove = false;
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }
}
