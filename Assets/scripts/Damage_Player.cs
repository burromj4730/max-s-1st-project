using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Player : MonoBehaviour
{
    public LayerMask mask;

    public int damage;
   
    public bool destroyUponContact = false;

    public GameObject destroyParticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer == 6)
        {
            int direction = collision.transform.position.x < transform.position.x ? -1 : 1;
            PlayerHealthSystem phs = collision.gameObject.GetComponent<PlayerHealthSystem>();
            phs.DoDamage(damage, direction);
            if (phs.health <= 0)
            {
                MagmaWanderer ai = GetComponent<MagmaWanderer>();
                if (ai != null)
                {
                    ai.canMove = false;
                    ai.magmaWanderer.SetBool("LMAO", true);
                    if (ai.direction < 0 && collision.transform.position.x > transform.position.x)
                    {
                        ai.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else if (ai.direction>0&& collision.transform.position.x < transform.position.x)
                    {
                        ai.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
               
            }
            
        }
        if (destroyUponContact)
        {
            if (destroyParticle != null)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GameObject particles = Instantiate(destroyParticle, this.transform);
                Invoke("DestroyThis", destroyParticle.GetComponent<ParticleSystem>().startLifetime);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    void DestroyThis()
    {
        Destroy(this.gameObject);
    } 
}
