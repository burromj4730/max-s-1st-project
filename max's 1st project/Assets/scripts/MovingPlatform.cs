using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    bool movingUp = true;

    public float topPos, botPos;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp && transform.position.y < topPos)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime);
        }
    }
}
