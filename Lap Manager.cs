using System.ComponentModel.Design;
using System.Xml.Serialization;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LapManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI lapTimerText;
    [SerializeField] TextMeshProUGUI lapCounterText;

    public int playerLaps;
    float playerLapTime;
    float totalSessionTime;

    public int AILaps;
    public bool isAIFinished = false;

    string activeTrack;

    // Function called once per frame
    void Update()
    {
        // Measuring lap time of player
        playerLapTime += Time.deltaTime;
        // Calling function to convert time into correct format to be displayed
        lapTimerText.text = TimeFormatter.FormatLapTime(playerLapTime);

        // Measuring total session time
        totalSessionTime += Time.deltaTime;
    }

    // Function is called everytime the player crosses Start/Finish line
    public void UpdateLapCounter(){
        // Checking if player finished first or second
        if(playerLaps == 6){
            if(isAIFinished){
                PlayerPrefs.SetInt("Finished", 2);
            }else{
                PlayerPrefs.SetInt("Finished", 1);
            }
            
            // Storing what track the player is on
            activeTrack = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("activeTrack", activeTrack);

            //Storing total session time
            PlayerPrefs.SetFloat("totalSession", totalSessionTime);
            SceneManager.LoadScene("Finish Screen");
        }

        // Displaying number of laps completed
        lapCounterText.text = string.Format("Lap {0}/5", playerLaps);
    }

    // Function is called everytime the player crosses Start/Finish line
    public void ResetLapTime(){
        // Getting the fastest lap set so far in previous laps, initially 360
        float fastestLap = PlayerPrefs.GetFloat("fastLap");

        // Checking if player has gone faster than any of his previous laps
        if(playerLapTime < fastestLap){
            // Storing fastest lap so far in the session
            playerLapTime = Mathf.Round(playerLapTime * 1000) / 1000;
            PlayerPrefs.SetFloat("fastLap", playerLapTime);
        }
        
        // Resetting lap time to measure the next lap
        playerLapTime = 0;
    }
}
