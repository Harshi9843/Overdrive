using UnityEngine;

public class BrakingAI : MonoBehaviour
{

    // Functions that runs when the AI car is in a braking zone
    private void OnTriggerEnter(Collider other)
    {
        // Getting the AI car controller script
        AICarController car = other.GetComponent<AICarController>();

        // Letting the AI car controller script know that the car is in the braking zone
        if(car){
            car.insideBrakingZone = true;
            
        } 
    }

    // Function that runs when the AI car is out of braking zone
    private void OnTriggerExit(Collider other)
    {   
        // Getting the AI car controller script
        AICarController car = other.GetComponent<AICarController>();
        
        // Letting the AI car controller script know that the car is not in the braking zone 
        if(car){
            car.insideBrakingZone = false;
        }
    }
}
