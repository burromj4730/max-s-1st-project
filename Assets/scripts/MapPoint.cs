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
}
