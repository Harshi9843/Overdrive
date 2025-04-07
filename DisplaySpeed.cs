using TMPro;
using UnityEngine;

public class DisplaySpeed : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speed;
    public Rigidbody playerCar;

    float currentSpeed;

    // Start is called once the game objects are loaded, and once before the first execution of Update 
    void Start()
    {
        speed = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Getting speed of car and displaying it
        currentSpeed = playerCar.linearVelocity.magnitude;
        speed.text = Mathf.Round(currentSpeed).ToString();
    }
}
