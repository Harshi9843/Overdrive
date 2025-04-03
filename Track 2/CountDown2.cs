using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown2 : MonoBehaviour
{

    public PlayerCarInput playerCar;
    public AICarController2 AICar;
    public LapManager lapManager;

    public TextMeshProUGUI countDownText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        lapManager = GetComponent<LapManager>();

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countDownText.gameObject.SetActive(true);
        AICar.enabled = false;
        playerCar.enabled = false;
        

        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "GO!";

        AICar.enabled = true;
        playerCar.enabled = true;
        lapManager.enabled = true;

        yield return null;
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
        

    }

}
