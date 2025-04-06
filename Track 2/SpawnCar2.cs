using UnityEngine;

public class SpawnCar2 : MonoBehaviour
{
    private int carPointer;
    [SerializeField] CarList carList;
    
    [SerializeField] CameraController cameraController; 
    [SerializeField] Countdown2 countdown;
    [SerializeField] DisplaySpeed displaySpeed;

    void Awake()
    {
        carPointer = PlayerPrefs.GetInt("pointer");
        Vector3 carPosition = new Vector3(548.21f, 0.06f, 397.4f);
        Quaternion carRotation = Quaternion.Euler(0, 88.749f, 0);

        GameObject PlayerCar = Instantiate(carList.carsList[carPointer], carPosition, carRotation);
        PlayerCar.name = "Player Car";
        
        
        cameraController.playerCar = PlayerCar.transform;
        cameraController.carRB = PlayerCar.GetComponent<Rigidbody>();
        displaySpeed.playerCar = PlayerCar.GetComponent<Rigidbody>();
        countdown.playerCar = PlayerCar.GetComponent<PlayerCarInput>();

        PlayerPrefs.SetFloat("fastLap", 360f);
        
    }
}
