using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Control : MonoBehaviour
{
    public List<Transform> nextPoints = new List<Transform>();

    public Vector4 canMove;

    public bool startMoving;

    public Map_Level_Points startingPoint;

    public float mapMoveSpeed;

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
            if (input != Vector2.zero)
            {
                switch (canMove.x,canMove.y,canMove.z,canMove.w)
                {
                    case (1, 0, 0, 0):
                        if (input.x == -1)
                        {
                            input = Vector2.left;
                        }
                        else
                        {
                            input = Vector2.zero;
                        }
                        break;
                    case (0, 1, 0, 0):
                        if (input.y == -1)
                        {
                            input = Vector2.down;
                        }
                        else
                        {
                            input = Vector2.zero;
                        }
                        break;
                    case (0, 0, 1, 0):
                        if (input.y == 1)
                        {
                            input = Vector2.up;
                        }
                        else
                        {
                            input = Vector2.zero;
                        }
                        break;
                    case (0, 0, 0, 1):
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
               
                
                    startMoving = true;
                    GetComponent<Rigidbody>().velocity = new Vector3(input.x, 0, input.y) * mapMoveSpeed;
                    // this.transform.Translate(new Vector3(input.x, 0, input.y)*10);
                
            }
            else
            {
                startMoving = false;
            }
            
        }
    }

    public void UpdatePoints(List<Transform> newpoints, Vector4 newDirections, bool stopAt)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (!stopAt)
        {
            nextPoints = newpoints;
            canMove = newDirections;
            startMoving = false;
        }
        else
        {
            nextPoints = newpoints;
            canMove = newDirections;
            startMoving = false;
        }
    }
}
