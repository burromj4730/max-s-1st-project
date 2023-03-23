using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOtherColliderOnEnter : MonoBehaviour
{
    public Collider2D otherCollider;
    public float secondsToDisableFor = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("DisableOtherCollider");
        }
    }

    private IEnumerator DisableOtherCollider()
    {
        otherCollider.enabled = false;

        yield return new WaitForSeconds(secondsToDisableFor);

        otherCollider.enabled = true;
    }
}
