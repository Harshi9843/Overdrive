using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown2 : MonoBehaviour
{
    public PlayerCarInput playerCar;
    [SerializeField] AICarController2 AICar;
    [SerializeField] LapManager lapManager;

    [SerializeField] TextMeshProUGUI countDownText;

    // Start is called once after the game and the objects are loaded in
    void Start(){   
        lapManager = GetComponent<LapManager>();

        StartCoroutine(StartCountdown());
    }

    // Function is called to start the countdown
    IEnumerator StartCountdown(){
        // Making sure the player or the AI doesn't move during countdown
        countDownText.gameObject.SetActive(true);
        AICar.enabled = false;
        playerCar.enabled = false;
        
        // Starting the countdown
        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "GO!";

        // Allowing the player and the AI to start moving after countdown
        AICar.enabled = true;
        playerCar.enabled = true;
        
        // Allowing the HUD elements to start measuring, i.e, the lap timer, lap counter, speed display
        lapManager.enabled = true;

        // Stop displaying the countdown after its finished
        yield return null;
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
        

    }

}
