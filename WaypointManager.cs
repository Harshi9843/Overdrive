using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{

    public List<Transform> waypoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Adding all the waypoints to a list
        foreach(Transform waypoint in gameObject.GetComponentInChildren<Transform>()){
            waypoints.Add(waypoint);
        }
    }
}
