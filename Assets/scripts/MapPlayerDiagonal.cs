using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayerDiagonal : MonoBehaviour
{
    public Transform startingPoint;

    public List<MapPoint.AccessiblePoint> currentlyAccessible = new List<MapPoint.AccessiblePoint>();

    public float mapMoveSpeed;

    private Rigidbody rb;

    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(startingPoint.position.x, transform.position.y, startingPoint.position.z);
        currentlyAccessible = startingPoint.gameObject.GetComponent<MapPoint>().canMoveTo;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map Point"))
        {
            rb.velocity = Vector3.zero;
            currentlyAccessible = other.GetComponent<MapPoint>().canMoveTo;
            canMove = true;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<level_Selection>()!=null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(other.gameObject.GetComponent<level_Selection>().sceneIndex);
            }
        }
    }
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool validInput = false;

        Transform targetPoint = null;

        if (input.normalized != Vector2.zero && canMove)
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
            canMove = false;
        }
    }
}
