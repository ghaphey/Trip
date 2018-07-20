using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleProperties : MonoBehaviour {

    [SerializeField] private float objWidth;
    [SerializeField] private List<Transform> safeWaypoints;

    public float GetWidth()
    {
        return objWidth;
    }

    public List<Transform> GetSafeZones()
    {
        return safeWaypoints;
    }
}
