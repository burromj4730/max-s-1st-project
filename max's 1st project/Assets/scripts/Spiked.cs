using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked : MonoBehaviour
{
    public Sprite spiked;

    public GameObject deathParticals;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerMovement>().canMove = false;
            collision.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = spiked;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), 15) * 50f);
            StartCoroutine(ExplodePlayer(collision.gameObject));
        }
    }
    IEnumerator ExplodePlayer(GameObject player)
    {
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(fadeToWhite(player));
        yield return new WaitForSeconds(0.25f);
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Instantiate(deathParticals, player.transform);
        StartCoroutine(TurnOfSprite(player));
    }
    IEnumerator TurnOfSprite(GameObject player)
    {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
    }
    IEnumerator fadeToWhite(GameObject player)
    {
        Color transp = player.transform.GetChild(2).GetComponent<SpriteRenderer>().color;
        yield return new WaitForSeconds(0.025f);
        transp.a += 0.1f;
        player.transform.GetChild(2).GetComponent<SpriteRenderer>().color = transp;
        if (player.transform.GetChild(2).GetComponent<SpriteRenderer>().color.a !=1)
        {
            StartCoroutine(fadeToWhite(player));
        }
    }
    IEnumerable screenShake(GameObject player)
    {
        yield return new 
    }
}

