using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
   
    public Transform tofollow;

    private bool justCollided;

    // Update is called once per frame
    void Update()
    {
        if (!justCollided)
        {
            if (tofollow.position.x >= transform.position.x)
            {
                transform.parent = tofollow;
                transform.position = new Vector3(tofollow.position.x, tofollow.position.y, transform.position.z);
                justCollided = true;
            }

            else
            {
                transform.position = new Vector3(transform.position.x, tofollow.position.y, transform.position.z);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent = null;
        StartCoroutine(collisionWait());
    }
    IEnumerator collisionWait()
    {
        yield return new WaitForSeconds(0.1f);
        justCollided = false;
    }
}
