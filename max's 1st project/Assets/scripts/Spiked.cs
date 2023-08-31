using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked : MonoBehaviour
{
    public Sprite spiked;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerMovement>().canMove = false;
            collision.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = spiked;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), 15) * 50f);
        }
    }
}
