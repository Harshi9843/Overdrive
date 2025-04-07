using UnityEngine;


// This class controls the main functions of every car in the game 
public class CarManager : MonoBehaviour
{
    float brakeInput; 

    // Public funciton that is accessed by other classes to give a car its movement functions
    public void SetInputs(Rigidbody CarBody, float throttleInput, float steeringAngle, float motorPower, float brakePower, WheelCollider[] colliders, Transform[] meshes){

        // Calls all the methods the give the car its functions
        ApplyThrottle(throttleInput, motorPower, colliders);
        ApplyBrakes(colliders, brakePower, throttleInput, CarBody);
        ApplySteering(colliders, steeringAngle);
        UpdateWheels(colliders, meshes);
    }


    // This function allows the car to accelerate  
    void ApplyThrottle(float throttleInput, float motorPower, WheelCollider[] colliders){

        // Gives power to all four wheels when the user presses W
        // The amount of power is dependent on the car
        for(int i = 0; i < 4; i++){
            colliders[i].motorTorque = throttleInput * motorPower;
        }
    }


    // This function allows the car to brake
    void ApplyBrakes(WheelCollider[] colliders, float brakePower, float throttleInput, Rigidbody CarBody){

        // This calculates which direction the car is moving
        float movingDirection = Vector3.Dot(transform.forward, CarBody.linearVelocity);

        // This code determines when the brakes need to be applied
        // If the car is reversing and the W (forward) key is pressed, the car brakes
        if(movingDirection < -0.5f && throttleInput > 0){ 
            brakeInput = Mathf.Abs(throttleInput);
        }
        // If the car is moving forward and the S (reverse) key is pressed, the car brakes
        else if(movingDirection > 0f && throttleInput < 0){
            brakeInput = Mathf.Abs(throttleInput);
        }
        // If the car is moving in the same direction as the input requires, the car does not brake
        else{
            brakeInput = 0;
        }

        // Applies the brake on all four wheels as determined above
        for(int i = 0; i < 4; i++){
            colliders[i].brakeTorque = brakePower * brakeInput;
        }
    }

    // This function allows the car to turn
    void ApplySteering(WheelCollider[] colliders, float steeringAngle){
        // Applies the steer angle to the front two wheels
        for(int i = 0; i < 4; i++){
            if(i == 0 || i == 1){ 
                colliders[i].steerAngle = steeringAngle;
            }
        }
    }


    // This function is used to visually show the car's wheels rotating and turning. 
    void UpdateWheels(WheelCollider[] colliders, Transform[] meshes){
        Vector3 pos;
        Quaternion rot;

        // Getting the rotation and position of the wheel collider (which you cannot see) and applying it to wheel meshes (what you see)
        for(int i = 0; i < 4; i ++){
            colliders[i].GetWorldPose(out pos, out rot);
            meshes[i].position = pos;
            meshes[i].rotation = rot;
        }
    }


    // This function applies a given amount of downforce to the car
    public void ApplyDownForce(float maxDownforce, float maxSpeed, float currentSpeed, Rigidbody playerCarBody){

        // Calculating the amount of downforce that will be applied
        // Closer to max speed = Greater amount of downforce and vice versa
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, currentSpeed);
        float downforceAmount = maxDownforce * speedFactor;

        // Applying downforce on the car 
        playerCarBody.AddForce(-transform.up * downforceAmount);
    }
}
