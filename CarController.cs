using System;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    public TextMeshProUGUI speedDisplay;
    public GameObject car;
    public Rigidbody playerCar;
    public WheelCollider[] colliders;
    public Transform[] meshes;

    public float throttleInput;
    public float steeringInput;
    public float brakeInput;

    public float maxDownforce;
    public float motorPower;
    public float brakePower;

    public AnimationCurve steeringRange;
    private float speedValue;
    public float speed{
        get{
            return speedValue;
        }
        set{
            speedValue = value;

            speedDisplay.text = "Speed: " + Math.Round(speed, 0);
        }
    }

    public float maxSpeed;


    // Update is called once per frame
    void Update()
    {
        speed = playerCar.linearVelocity.magnitude;
        Inputs();
        ApplyThrottle();
        ApplyBrakes();
        ApplySteering();
        UpdateWheels();
    }

    void FixedUpdate()
    {
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, speed);
        float downforceAmount = maxDownforce * speedFactor;
        playerCar.AddForce(-transform.up * downforceAmount);
    }

    void Inputs(){
        throttleInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        float movingDirection = Vector3.Dot(transform.forward, playerCar.linearVelocity);
        if(movingDirection < -0.5f && throttleInput > 0){
            brakeInput = Mathf.Abs(throttleInput);
        }
        else if(movingDirection > 0f && throttleInput < 0){
            brakeInput = Mathf.Abs(throttleInput);
        }
        else{
            brakeInput = 0;
        }

    }

    void ApplyThrottle(){
        for(int i = 0; i < 4; i++){
            colliders[i].motorTorque = throttleInput * motorPower;
        }
    }

    void ApplyBrakes(){
        for(int i = 0; i < 4; i++){
            colliders[i].brakeTorque = brakePower * brakeInput;
        }
    }

    void ApplySteering(){
        for(int i = 0; i < 4; i++){
            if(i == 0 || i == 1){ 
                colliders[i].steerAngle = steeringInput * steeringRange.Evaluate(speed);
                // colliders[i].steerAngle = steeringInput * 60f;
            }
        }
    }

    void UpdateWheels(){
        Vector3 pos;
        Quaternion rot;
        for(int i = 0; i < 4; i ++){
            colliders[i].GetWorldPose(out pos, out rot);
            meshes[i].position = pos;
            meshes[i].rotation = rot;
        }
    }
}
