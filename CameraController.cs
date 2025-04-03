using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public Transform playerCar;
    public Rigidbody carRB;

    public Vector3 offset;
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        // The player car's forward vector
        Vector3 playerForward = (carRB.linearVelocity + playerCar.forward).normalized;

        // Makes the camera smoothly follow the car
        transform.position = Vector3.Lerp(transform.position, playerCar.position + playerCar.TransformVector(offset) + playerForward * (-5f), speed * Time.deltaTime);
        
        // Ensures camera always looks at the car no matter where it moves
        transform.LookAt(playerCar);
    }

    
}
