using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPipePiece : MonoBehaviour
{
    public TransportPipePiece nextPipe;

    Vector2 nextPipeDir;

    private void Start()
    {
        if (nextPipe != null)
        {
            nextPipeDir = nextPipe.transform.position - transform.position;
        }
        else
        {
            Debug.LogWarning("Transport pipe: " + this.name + ".\nHas no next pipe reference!");
        }
        //Debug.Log(nextPipeDir.normalized);
    }

    public Vector2 GetNextPipeDirection()
    {
        if (nextPipe != null)
        {
            return nextPipeDir.normalized;
        }
        else
        {
            return Vector2.zero;
        }
    }
}
