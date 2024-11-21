using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    [System.Serializable]
    public struct AccessiblePoint
    {
        public Transform accessiblePoint;
        public List<Vector2> inputsToAccess;
    }

    public List<AccessiblePoint> canMoveTo = new List<AccessiblePoint>();
    private void Awake()
    {
        foreach (AccessiblePoint ap in canMoveTo)
        {
            if (ap.accessiblePoint.parent.GetChild(0).GetComponent<level_Selection>().MState == level_Selection.LevelState.LOCKED)
            {
                canMoveTo.Remove(ap);
            }
        }
    }
}
