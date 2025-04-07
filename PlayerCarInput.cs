using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using TMPro;
using System;

public class PlayerCarInput : MonoBehaviour
{
    Rigidbody playerCarBody;
    CarManager carManager;
    [SerializeField] WheelCollider[] colliders;
    [SerializeField] Transform[] meshes;

    [SerializeField] float motorPower;
    [SerializeField] float brakePower;

    float throttleInput;
    float steeringInput;

    [SerializeField] AnimationCurve steeringRange;
    float steerAngle;

    [SerializeField] float maxSpeed; 
    [SerializeField] float maxDownforce;
    [SerializeField] float currentSpeed;
    

    // Start is called once before the first execution of Update and after the game objects are loaded in
    void Start() 
    {   
        // Getting car controller class to use its functions
        carManager = GetComponent<CarManager>();

        // Getting the car's Rigid Body, which is the component onto which physics is applied 
        playerCarBody = gameObject.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {   
        // Calulating speed of the car
        currentSpeed = playerCarBody.linearVelocity.magnitude;
        
        // Getting User Input, "Vertical" axix: W and S, and "Horizontal Axix": A and D
        throttleInput = Input.GetAxis("Vertical");  // Accelerating and braking inputs
        steeringInput = Input.GetAxis("Horizontal"); // Turning inputs

        // Calulates the steering angle based on the speed of the car
        // Higher speed =  Lower steering angle, Lower speed = Higher steer angle
        steerAngle = steeringInput * steeringRange.Evaluate(currentSpeed);

        // Calling a function from the general car controller that gives the car movement, with parameters specific for this car
        carManager.SetInputs(playerCarBody, throttleInput, steerAngle , motorPower, brakePower, colliders, meshes);
    }


    // Fixed Update is called at regular intervals defined by the physics settings.
    // Doesn't depend on the frame rate
    void FixedUpdate() 
    {
        // Calling a pre-defined function from the general car controller class to apply specific amount of downforce for this car
        carManager.ApplyDownForce(maxDownforce, maxSpeed, currentSpeed, playerCarBody);
    }
}
