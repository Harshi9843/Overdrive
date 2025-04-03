using UnityEngine;

public class BrakingAI2 : MonoBehaviour
{

    GameObject brakingZone;

    // Functions that runs when the AI car is in a braking zone
    private void OnTriggerEnter(Collider other)
    {
        // Getting the AI car controller script
        AICarController2 car = other.GetComponent<AICarController2>();

        // Letting the AI car controller script know that the car is in the braking zone
        if(car){

            car.big = gameObject;
            car.insideBraking = true;
            
        }
    }

    // Function that runs when the AI car is out of braking zone
    private void OnTriggerExit(Collider other)
    {   
        // Getting the AI car controller script
        AICarController2 car = other.GetComponent<AICarController2>();
        
        // Letting the AI car controller script know that the car is not in the braking zone 
        if(car){
            car.insideBraking = false;
        }
    }
}
