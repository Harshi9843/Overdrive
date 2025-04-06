using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class CarSelectionManager : MonoBehaviour
{
    [SerializeField] Transform carStand;
    [SerializeField] float rotationSpeed;
    [SerializeField] VehicleList CarList;
    [SerializeField] int vehiclePointer;
    [SerializeField] GameObject newCar;

    [SerializeField] TextMeshProUGUI topSpeed;
    [SerializeField] TextMeshProUGUI horsePower;
    [SerializeField] TextMeshProUGUI downforce;


    // Function is called before game and other objects are loaded
    void Awake()
    {
        // Displaying the first car
        PlayerPrefs.SetInt("pointer", 0);
        vehiclePointer = PlayerPrefs.GetInt("pointer");
        newCar = Instantiate(CarList.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity);
        newCar.transform.parent = carStand.transform;

        // Displaying the first's car's information
        topSpeed.text = "Top Speed: 60mph";
        horsePower.text = "Horse Power: 150hp";
        downforce.text = "Downforce: 600kg";
    }


    // Function is called at fixed time intervals 
    void FixedUpdate()
    {
        // Rotating the car 360°
        carStand.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }


    // Function is called when right arrow button is clicked
    public void Rightbutton(){
        // Displaying the next car, and its infomation, if there is one
        if(vehiclePointer < CarList.vehicles.Length - 1){ 
            Destroy(newCar);
            vehiclePointer++;
            PlayerPrefs.SetInt("pointer", vehiclePointer);

            newCar = Instantiate(CarList.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            newCar.transform.parent = carStand.transform;

            if(vehiclePointer == 1 ){
                topSpeed.text = "Top Speed: 90mph";
                horsePower.text = "Horse Power: 300hp";
                downforce.text = "Downforce: 900kg";
            }

        }
    }


    // Function is called when the left arrow button is clicked
    public void Leftbutton(){
        // Displays the previous car until the first car is reached
        if(vehiclePointer > 0){
            Destroy(newCar);
            vehiclePointer--;
            PlayerPrefs.SetInt("pointer", vehiclePointer);

            newCar = Instantiate(CarList.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            newCar.transform.parent = carStand.transform;

            if(vehiclePointer == 0 ){
                topSpeed.text = "Top Speed: 60mph";
                horsePower.text = "Horse Power: 150hp";
                downforce.text = "Downforce: 600kg";
            }
        }
    }


    // Function is called when the player clicks Select
    public void StoreCar(){
        // Storing the car selected by the player
        PlayerPrefs.SetInt("chosenCar", vehiclePointer);
        print(PlayerPrefs.GetInt("chosenCar"));

        // Loading the track selection menu
        SceneManager.LoadScene("Track Selection Menu");
    }
}
