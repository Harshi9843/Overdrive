using TMPro;
using UnityEngine;

public class DisplaySpeed : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speed;
    public Rigidbody playerCar;

    float currentSpeed;

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
