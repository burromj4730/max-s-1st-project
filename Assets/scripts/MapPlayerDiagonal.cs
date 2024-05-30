using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerDiagonal : MonoBehaviour
{
    public Transform startingPoint;
    public List<MapPoint.AccessiblePoint> currentlyAccessible = new List<MapPoint.AccessiblePoint>();
    public float mapMoveSpeed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(startingPoint.position.x, transform.position.y, startingPoint.position.z);
        currentlyAccessible = startingPoint.gameObject.GetComponent<MapPoint>().canMoveTo;
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = Vector3.zero;
        currentlyAccessible = other.GetComponent<MapPoint>().canMoveTo;
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool validInput = false;

        Transform targetPoint = null;

        if (input.normalized != Vector2.zero)
        {
            foreach (MapPoint.AccessiblePoint point in currentlyAccessible)
            {
                foreach (Vector2 allowedInput in point.inputsToAccess)
                {
                    if (input.normalized == allowedInput)
                    {
                        validInput = true; 
                        targetPoint = point.accessiblePoint;
                        break;
                    }
                }
                if (validInput)
                {
                    break;
                }
            }
        }

        if (validInput)
        {
            Vector3 direction = targetPoint.position - transform.position;

            rb.velocity = direction.normalized * mapMoveSpeed;
        }
    }
}
