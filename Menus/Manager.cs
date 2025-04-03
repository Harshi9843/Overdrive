using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Transform stand;
    public float rotationSpeed;
    public VehicleList CarList;
    public int vehiclePointer;
    public GameObject newCar;

    // Text elements
    public TextMeshProUGUI topSpeed;
    public TextMeshProUGUI horsePower;
    public TextMeshProUGUI downforce;

    void Awake()
    {
        PlayerPrefs.SetInt("pointer", 0);
        vehiclePointer = PlayerPrefs.GetInt("pointer");
        newCar = Instantiate(CarList.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
        newCar.transform.parent = stand.transform;

        topSpeed.text = "Top Speed: 60mph";
        horsePower.text = "Horse Power: 150hp";
        downforce.text = "Downforce: 600kg";
    }

    void FixedUpdate()
    {
        stand.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void Rightbutton(){
        if(vehiclePointer < CarList.vehicles.Length - 1){
            Destroy(newCar);
            vehiclePointer++;
            PlayerPrefs.SetInt("pointer", vehiclePointer);

            newCar = Instantiate(CarList.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            newCar.transform.parent = stand.transform;

            if(vehiclePointer == 1 ){
                topSpeed.text = "Top Speed: 90mph";
                horsePower.text = "Horse Power: 300hp";
                downforce.text = "Downforce: 900kg";
            }

        }
    }
    public void Leftbutton(){
        if(vehiclePointer > 0){
            Destroy(newCar);
            vehiclePointer--;
            PlayerPrefs.SetInt("pointer", vehiclePointer);

            newCar = Instantiate(CarList.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            newCar.transform.parent = stand.transform;

            if(vehiclePointer == 0 ){
                topSpeed.text = "Top Speed: 60mph";
                horsePower.text = "Horse Power: 150hp";
                downforce.text = "Downforce: 600kg";
            }
        }
    }

    public void StoreCar(){
        PlayerPrefs.SetInt("chosenCar", vehiclePointer);
        print(PlayerPrefs.GetInt("chosenCar"));

        SceneManager.LoadScene("Track Selection Menu");
    }
}
