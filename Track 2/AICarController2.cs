using System;
using UnityEngine;

public class AICarController2 : AICarController
{
    public GameObject brakingZone;
    [SerializeField] GameObject bigBraking;
    [SerializeField] GameObject bigBraking2;

    // Override Update to extend base behavior
    void Update()
    {
        // Getting the speed of the car every frame
        currentSpeed = CarBody.linearVelocity.magnitude;

        // Waypoint switching logic
        if (Vector3.Distance(waypoints[currentWaypoint].position, transform.position) < switchWaypointDist)
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }

        Vector3 fwdDirection = transform.TransformDirection(Vector3.forward);
        currentAngle = Vector3.SignedAngle(fwdDirection, waypoints[currentWaypoint].position - transform.position, Vector3.up);

        throttleInput = Mathf.Clamp01(1f - Math.Abs(currentSpeed * 0.06f * currentAngle) / maxAngle);

        if (insideBrakingZone)
        {
            if (bigBraking.transform.position == brakingZone.transform.position || bigBraking2.transform.position == brakingZone.transform.position)
            {
                throttleInput = -1f;
            }
            else
            {
                throttleInput = -throttleInput * Mathf.Clamp01(currentSpeed / maxSpeed * 2f - 1f);
            }
        }

        carManager.SetInputs(CarBody, throttleInput, currentAngle, motorPower, brakePower, wheel_colliders, wheel_meshes);

        Debug.DrawRay(transform.position, waypoints[currentWaypoint].position - transform.position, Color.yellow);
    }
}
