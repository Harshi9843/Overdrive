using TMPro;
using UnityEngine;

public class DisplaySpeed : MonoBehaviour
{
    public TextMeshProUGUI speed;
    public Rigidbody playerCar;

    public float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = playerCar.linearVelocity.magnitude;
        speed.text = Mathf.Round(currentSpeed).ToString();
    }
}
