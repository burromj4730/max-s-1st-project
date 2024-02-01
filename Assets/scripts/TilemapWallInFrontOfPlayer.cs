using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapWallInFrontOfPlayer : MonoBehaviour
{
    public Tilemap map;

    public float fadeOutSpeed;

    public float fadeInSpeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FadeOut();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FadeIn();
        }
    }

    private void FadeOut()
    {
        if (map.color.a > 0f)
        {
            StartCoroutine("FadingOut");
        }
    }

    IEnumerator FadingOut()
    {
        Vector4 newCol = map.color;
        newCol.w -= fadeOutSpeed;
        map.color = newCol;

        yield return new WaitForSeconds(0.01f);
        FadeOut();
    }
    private void FadeIn()
    {
        if (map.color.a < 1f)
        {
            StartCoroutine("FadingIn");
        }
    }

    IEnumerator FadingIn()
    {
        Vector4 newCol = map.color;
        newCol.w += fadeOutSpeed;
        map.color = newCol;

        yield return new WaitForSeconds(0.01f);
        FadeIn();
    }
}
