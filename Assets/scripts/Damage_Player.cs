using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Player : MonoBehaviour
{
    public LayerMask mask;

    public int damage;

    public Player_Health HP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == mask)
        {
            HP.TakeDamage(damage);
        }
    }
}
