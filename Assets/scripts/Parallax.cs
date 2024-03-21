using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float amount;

    public Camera camera;

    private float startingPos;

    private float spriteLength;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position.x;
        spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = camera.transform.position;
        float check = position.x * (1 - amount);
        float distance = position.x * amount;
        Vector3 newPosition = new Vector3(startingPos + distance, transform.position.y, transform.position.z);
        transform.position = newPosition;
        if (check > startingPos + (spriteLength / 2))
        {
            startingPos += spriteLength;

        }
        else if (check < startingPos - (spriteLength / 2))
        {
            startingPos -= spriteLength;

        }
    }
}
