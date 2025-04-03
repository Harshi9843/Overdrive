using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    private int carPointer;
    public CarList carList;
    
    public CameraController cameraController; 
    public Countdown countdown;
    public DisplaySpeed displaySpeed;

    void Awake()
    {
        carPointer = PlayerPrefs.GetInt("pointer");
        Vector3 carPosition = new Vector3(-16.66f, 0.41f, 61.49f);
        Quaternion carRotation = Quaternion.Euler(0, 88, 0);

        GameObject PlayerCar = Instantiate(carList.carsList[carPointer], carPosition, carRotation);
        PlayerCar.name = "Player Car";
        
        
        cameraController.playerCar = PlayerCar.transform;
        cameraController.carRB = PlayerCar.GetComponent<Rigidbody>();
        displaySpeed.playerCar = PlayerCar.GetComponent<Rigidbody>();
        countdown.playerCar = PlayerCar.GetComponent<PlayerCarInput>();

        PlayerPrefs.SetFloat("fastLap", 360f);
        
    }
}
