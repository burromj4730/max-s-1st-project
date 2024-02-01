using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfrontOfPlayer : MonoBehaviour
{
    public SpriteRenderer SR;

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
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FadeOut()
    {
        if (SR.color.a > 0f)
        {
            StartCoroutine("FadingOut");
        }
    }

    IEnumerator FadingOut()
    {
        Vector4 newCol = SR.color;
        newCol.w -= fadeOutSpeed;
        SR.color = newCol;

        yield return new WaitForSeconds(0.01f);
        FadeOut();
    }
    private void FadeIn()
    {
        if(SR.color.a < 1f)
        {
            StartCoroutine("FadingIn");
        }
    }
    
    IEnumerator FadingIn()
    {
        Vector4 newCol = SR.color;
        newCol.w += fadeOutSpeed;
        SR.color = newCol;

        yield return new WaitForSeconds(0.01f);
        FadeIn();
    }
}
