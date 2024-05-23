using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Level_Points : MonoBehaviour
{
    public List<Transform> nextPoints = new List<Transform>();

    public Vector4 canMove;

    public bool stopAt;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Map_Control>().UpdatePoints(nextPoints, canMove, stopAt);
    }
}
