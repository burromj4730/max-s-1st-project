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
        }
        else
        {
            agro = false;
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
        RB.velocity = new Vector2(direction * moveSpeed, 0);
    }
    private void Wander()
    {
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position - new Vector3(0.5f, 0), -Vector2.up, 0.6f, mask);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0), -Vector2.up, 0.6f, mask);
        if (hitleft.collider == null)
        {
            direction = 1;
            magmaWanderer.SetBool("Turn Right", true);
            StartCoroutine(WaitForAnimation("Turn Right", true));
        }
        if (hitright.collider == null)
        {
            direction = -1;
            magmaWanderer.SetBool("Turn Left", true);
            StartCoroutine(WaitForAnimation("Turn Left", false));
        }
        
    }

    private void Agro(GameObject Player)
    {
        if (Player.transform.position.x < transform.position.x)
        {
            direction = -1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            direction = 1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    IEnumerator WaitForAnimation(string BoolName, bool flip)
    {
        yield return new WaitForSeconds(0.3f);
        magmaWanderer.SetBool(BoolName, false);
        GetComponent<SpriteRenderer>().flipX = flip;
    }
}
