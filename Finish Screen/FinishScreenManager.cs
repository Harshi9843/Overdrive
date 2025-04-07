using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreenManager : MonoBehaviour
{ 
    [SerializeField] TextMeshProUGUI fastestLapText;
    [SerializeField] TextMeshProUGUI sessionTimeText;
    [SerializeField] TextMeshProUGUI firstPosition;
    [SerializeField] TextMeshProUGUI secondPosition;

    [SerializeField] LapTimesDB db; 

    float fastestLap;
    float totalSessionTime;
    int playerPosition;
    
    string playerName;
    string activeTrack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        fastestLap = PlayerPrefs.GetFloat("fastLap");             // Getting Fastest lap
        totalSessionTime = PlayerPrefs.GetFloat("totalSession");  // Getting total session time
        playerName = PlayerPrefs.GetString("playerName");         // Getting username
        playerPosition = PlayerPrefs.GetInt("Finished");          // Getting the position the player finished in
        activeTrack = PlayerPrefs.GetString("activeTrack");  

        // Sending the fastest lap to determine if it is a player record or lap record 
        db.SubmitLapTime(playerName, fastestLap, activeTrack);  

        // Displaying the player and AI car in position leaderboard, in the order they finished
        if(playerPosition == 1){
            firstPosition.text = playerName;
            secondPosition.text = "AI Car";
        }else{
            firstPosition.text = "AI Car";
            secondPosition.text = playerName;
        }
        
        // Displaying fastest lap time and total session time 
        fastestLapText.text = "Fastest Lap: " + TimeFormatter.FormatLapTime(fastestLap);
        sessionTimeText.text = "Total Session Time: " + TimeFormatter.FormatLapTime(totalSessionTime);


    }

    // Function called when proceed button is clicked on the finish screen
    public void Proceed(){
        SceneManager.LoadScene("Main Menu");
    }
}
