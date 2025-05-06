using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AICarController : MonoBehaviour
{
    protected CarManager carManager;
    protected Rigidbody CarBody;  

    [SerializeField] protected WheelCollider[] wheel_colliders;
    [SerializeField] protected Transform[] wheel_meshes;

    [SerializeField] protected WaypointManager waypointManager;
    [SerializeField] protected List<Transform> waypoints;

    [SerializeField] protected int currentWaypoint;
    [SerializeField] protected int switchWaypointDist; 

    [SerializeField] protected float maxDownforce;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float maxAngle;

    [SerializeField] protected float motorPower;
    protected float throttleInput;

    protected float currentSpeed;
    protected float currentAngle;
    
    public bool insideBrakingZone;
    [SerializeField] protected float brakePower;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // Getting the main car controller script
        carManager = GetComponent<CarManager>();
        CarBody = gameObject.GetComponent<Rigidbody>();

        // Getting the list of waypoints
        waypoints = waypointManager.waypoints;
        // Setting the current waypoint, which the AI car follows, to the first waypoint on the track 
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the speed of the car every frame
        currentSpeed = CarBody.linearVelocity.magnitude;

        // This code allows the AI to follow the next waypoint once its close enough to the current waypoint
        if(Vector3.Distance(waypoints[currentWaypoint].position, transform.position) < switchWaypointDist){
            currentWaypoint++;
            if(currentWaypoint == waypoints.Count){
                currentWaypoint = 0;
            }
        }

        // This is the forward direction of the car relative to the other objects
        Vector3 fwdDirection = transform.TransformDirection(Vector3.forward);

        // Calculates the angle between the waypoint and the car (How much the car should steer)
        currentAngle = Vector3.SignedAngle(fwdDirection, waypoints[currentWaypoint].position - transform.position, Vector3.up);

        // This controls the percentage of throttle the AI car uses
        throttleInput = Mathf.Clamp01(1f - Math.Abs(currentSpeed * 0.1f * currentAngle)/maxAngle);

        // This controls the percentage of brake the AI car applies
        if(insideBrakingZone){
            throttleInput = -throttleInput * Mathf.Clamp01(currentSpeed / maxSpeed * 2 - 1f);
        }

        // Calls a method from the car controller class with parameters specfic to this car, to give the car its functions
        carManager.SetInputs(CarBody, throttleInput, currentAngle, motorPower ,brakePower, wheel_colliders, wheel_meshes);

        Debug.DrawRay(transform.position, waypoints[currentWaypoint].position - transform.position, Color.yellow);

    }

    // This function is run at regular intervals determined by the physics engine 
    void FixedUpdate()
    {
        // Calls a method from the car controller class that applies a specified amount of downforce
        carManager.ApplyDownForce(maxDownforce, maxSpeed, currentSpeed, CarBody);
    }
}
