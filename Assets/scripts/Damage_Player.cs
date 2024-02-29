using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Player : MonoBehaviour
{
    public LayerMask mask;

    public int damage;

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == 6)
        {
            int direction = collision.transform.position.x < transform.position.x ? -1 : 1;
            collision.gameObject.GetComponent<PlayerHealthSystem>().DoDamage(damage, direction);
        }
    }
}
