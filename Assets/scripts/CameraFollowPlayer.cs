using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Vector3 MyPos;
    public Transform tofollow;
    public Camera Cam;
    

    // Update is called once per frame
    void Update()
    {
        transform.position = tofollow.position + MyPos;   
    }
}
