using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Control : MonoBehaviour
{
    private List<Transform> nextPoints = new List<Transform>();

    private Vector4 canMove;

    private bool startMoving;

    public Map_Level_Points startingPoint;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPoint.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startMoving)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            switch (canMove)
            {
                case Vector4 vec when vec.Equals(new Vector4(1, 0, 0, 0)):
                    if (input.x == -1)
                    {
                        input = Vector2.left;
                    }
                    else
                    {
                        input = Vector2.zero;
                    }
                    break;
                case Vector4 vec when vec.Equals(new Vector4(0, 1, 0, 0)):
                    if (input.y == -1)
                    {
                        input = Vector2.down;
                    }
                    else
                    {
                        input = Vector2.zero;
                    }
                    break;
                case Vector4 vec when vec.Equals(new Vector4(0, 0, 1, 0)):
                    if (input.y == 1)
                    {
                        input = Vector2.up;
                    }
                    else
                    {
                        input = Vector2.zero;
                    }
                    break;
                case Vector4 vec when vec.Equals(new Vector4(0, 0, 0, 1)):
                    if (input.x == 1)
                    {
                        input = Vector2.right;
                    }
                    else
                    {
                        input = Vector2.zero;
                    }
                    break;
                default:
                    break;
            }
            if (input != Vector2.zero)
            {
                startMoving = true;
                this.transform.Translate(new Vector3(input.x, 0, input.y)*10);
            }
        }
    }

    public void UpdatePoints(List<Transform> newpoints, Vector4 newDirections, bool stopAt)
    {
        if (!stopAt)
        {

        }
        else
        {
            nextPoints = newpoints;
            canMove = newDirections;
            startMoving = false;
        }
    }
}
