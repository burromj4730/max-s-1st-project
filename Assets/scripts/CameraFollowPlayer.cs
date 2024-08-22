using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
   
    public Transform tofollow;

    private bool justCollided;
//-------------------------------------------------------------

    [System.Serializable]
    public struct cameraFreeze
    {
        public float startFreezeX;
        public Vector2 freezePositionMin;
        public Vector2 freezePositionMax;
        public float endFreezeX;
        public bool islast;
    }

    public List<cameraFreeze> FreezePosition = new List<cameraFreeze>();
    public int currentPosition = 0;

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
        if (tofollow.position.x > FreezePosition[currentPosition].startFreezeX)
        {
            if(tofollow.position.x< FreezePosition[currentPosition].endFreezeX)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(((FreezePosition[currentPosition].freezePositionMax.x + FreezePosition[currentPosition].freezePositionMin.x)/2)-tofollow.position.x, ((FreezePosition[currentPosition].freezePositionMax.y + FreezePosition[currentPosition].freezePositionMin.y)/2)-tofollow.position.y,transform.position.z),1f);
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
