using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector2 amount;

    public Camera camera;

    private Vector2 startingPos;

    private float spriteLength;

    private float spriteHight;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteHight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = camera.transform.position;
        float check = position.x * (1 - amount.x);
        float distance = position.x * amount.x;
        float checkY = position.y * (1 - amount.y);
        float distanceY = position.y * amount.y;
        Vector3 newPosition = new Vector3(startingPos.x + distance, startingPos.y + distanceY, transform.position.z);
        transform.position = newPosition;
        if (check > startingPos.x + (spriteLength / 2))
        {
            startingPos.x += spriteLength;

        }
        else if (check < startingPos.x - (spriteLength / 2))
        {
            startingPos.x -= spriteLength;

        }
    }
}