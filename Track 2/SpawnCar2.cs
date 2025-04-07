using UnityEngine;

public class SpawnCar2 : MonoBehaviour
{
    private int carPointer;
    [SerializeField] CarList carList;
    
    [SerializeField] CameraController cameraController; 
    [SerializeField] Countdown2 countdown;
    [SerializeField] DisplaySpeed displaySpeed;

    // Function is called before any game objects are loaded in
    void Awake()
    {
        // Determining which car the player has chosen
        carPointer = PlayerPrefs.GetInt("pointer");

        // Spawning car in the correct position and making sure its facing the right way
        Vector3 carPosition = new Vector3(548.21f, 0.06f, 397.4f);
        Quaternion carRotation = Quaternion.Euler(0, 88.749f, 0);

        // Creating the car
        GameObject PlayerCar = Instantiate(carList.carsList[carPointer], carPosition, carRotation);
        PlayerCar.name = "Player Car";
        
        // Sending the components of the car spawned in to other scripts that need them
        cameraController.playerCar = PlayerCar.transform;
        cameraController.carRB = PlayerCar.GetComponent<Rigidbody>();
        displaySpeed.playerCar = PlayerCar.GetComponent<Rigidbody>();
        countdown.playerCar = PlayerCar.GetComponent<PlayerCarInput>();

        // Creating location where fastest lap can be stored
        PlayerPrefs.SetFloat("fastLap", 360f);
        
    }
}
