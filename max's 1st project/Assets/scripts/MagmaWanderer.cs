using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaWanderer : MonoBehaviour
{
    public Rigidbody2D RB;

    public float direction;

    public float moveSpeed;

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position - new Vector3(0.5f, 0), -Vector2.up, 0.6f, mask);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0), -Vector2.up, 0.6f, mask);
        if (hitleft.collider == null)
        {
            direction = 1;
        }
        if (hitright == null)
        {
            direction = -1;
        }
        RB.velocity = new Vector2(direction * moveSpeed, 0);
    }
}
