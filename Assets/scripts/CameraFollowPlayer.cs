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

    private bool bossCamOn = false;

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
        if (tofollow.position.x > FreezePosition[currentPosition].startFreezeX && tofollow.position.x < FreezePosition[currentPosition].endFreezeX)
        {
            bossCamOn = true;

        }

        else if (tofollow.position.x > FreezePosition[currentPosition].endFreezeX && FreezePosition[currentPosition].islast)
        {
            bossCamOn = false;

            cam.transform.localPosition = new Vector3(0f, 0f, cam.transform.localPosition.z);
            cam.orthographicSize = defaultCameraSize;
        }
        if (bossCamOn)
        {
            if (tofollow.position.x > FreezePosition[currentPosition].startFreezeX && !reachedRoomCentre)
            {
                tofollow.gameObject.GetComponent<playerMovement>().canMove = false;
                tofollow.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                Vector3 playerPos = tofollow.position;
                float newCamx = playerPos.x > FreezePosition[currentPosition].freezePosition.x ? Mathf.Abs(playerPos.x - FreezePosition[currentPosition].freezePosition.x) * -1 : Mathf.Abs(FreezePosition[currentPosition].freezePosition.x - playerPos.x);
                newCamx *= 0.5f;
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(newCamx, (FreezePosition[currentPosition].freezePosition.y) - tofollow.position.y, transform.position.z), moveTime);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, FreezePosition[currentPosition].cameraSize, moveTime);
                Debug.Log("newX: " + newCamx + "\ntransformX: " + transform.localPosition.x);
                if (Vector2.Distance(new Vector2(transform.localPosition.x, transform.position.y), new Vector2(newCamx, FreezePosition[currentPosition].freezePosition.y)) <= 0.1f)
                {
                    reachedRoomCentre = true;
                    tofollow.gameObject.GetComponent<playerMovement>().canMove = true;
                    tofollow.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    tofollow.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                }
            }
            else if (reachedRoomCentre)
            {
                Vector3 playerPos = tofollow.position;
                float newCamx = playerPos.x > FreezePosition[currentPosition].freezePosition.x ? Mathf.Abs(playerPos.x - FreezePosition[currentPosition].freezePosition.x) * -1 : Mathf.Abs(FreezePosition[currentPosition].freezePosition.x - playerPos.x);
                newCamx *= 0.5f;
                playerPos.x = 0f;
                transform.localPosition = new Vector3(newCamx, FreezePosition[currentPosition].freezePosition.y, transform.localPosition.z) - playerPos;

            }
            if (tofollow.position.x > FreezePosition[currentPosition].endFreezeX)
            {
                reachedRoomCentre = false;
                if (currentPosition + 1 != FreezePosition.Count)
                {
                    currentPosition++;
                }

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