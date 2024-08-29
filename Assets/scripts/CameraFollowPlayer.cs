using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
   
    public Transform tofollow;

    private bool justCollided;

    public float moveTime;

    private float defaultCameraSize;

    private Camera cam;

    private bool reachedRoomCentre;

    [System.Serializable]
    public struct cameraFreeze
    {
        public float startFreezeX;
        public Vector2 freezePosition;
        public float cameraSize;
        public float endFreezeX;
        public bool islast;
    }

    public List<cameraFreeze> FreezePosition = new List<cameraFreeze>();
    public int currentPosition = 0;

    private void Start()
    {
        cam = GetComponent<Camera>();
        defaultCameraSize = cam.orthographicSize;
    }
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
        if (tofollow.position.x > FreezePosition[currentPosition].startFreezeX && !reachedRoomCentre) 
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                 new Vector3((FreezePosition[currentPosition].freezePosition.x)  - tofollow.position.x,
                    (FreezePosition[currentPosition].freezePosition.y) - tofollow.position.y,
                        transform.position.z), moveTime*0.001f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, FreezePosition[currentPosition].cameraSize, moveTime);
            if (Vector2.Distance(transform.position, FreezePosition[currentPosition].freezePosition) > 0.1f)
            {
                reachedRoomCentre = true;
            }
        }
        else if(reachedRoomCentre)
        {
            Vector3 playerPos = tofollow.position;
            playerPos.z = transform.localPosition.z;
            Vector3 midPoint = new Vector3(FreezePosition[currentPosition].freezePosition.x, FreezePosition[currentPosition].freezePosition.y,transform.localPosition.z) - playerPos;
            midPoint = midPoint.normalized;
            midPoint *= Vector3.Distance(transform.position, playerPos) / 2;
            transform.localPosition = Vector3.Lerp(transform.localPosition, midPoint, moveTime / 2);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -21.3f);
        }
        if (tofollow.position.x > FreezePosition[currentPosition].endFreezeX)
        {
            currentPosition++;
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
