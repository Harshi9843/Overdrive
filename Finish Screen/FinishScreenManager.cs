using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreenManager : MonoBehaviour
{ 
    public TextMeshProUGUI fastestLapText;
    public TextMeshProUGUI sessionTimeText;
    public TextMeshProUGUI firstPosition;
    public TextMeshProUGUI secondPosition;

    public LapTimesDB db; 

    float fastestLap;
    float totalSessionTime;

    int playerPosition;

    string playerName;

    private string activeTrack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fastestLap = PlayerPrefs.GetFloat("fastLap");
        totalSessionTime = PlayerPrefs.GetFloat("totalSession");
        playerName = PlayerPrefs.GetString("playerName");
        playerPosition = PlayerPrefs.GetInt("Finished");

        activeTrack = PlayerPrefs.GetString("activeTrack");

        db.SubmitLapTime(playerName, fastestLap, activeTrack);

        if(playerPosition == 1){
            firstPosition.text = playerName;
            secondPosition.text = "AI Car";
        }else{
            firstPosition.text = "AI Car";
            secondPosition.text = playerName;
        }

        fastestLapText.text = "Fastest Lap: " + FormatTime(fastestLap);
        sessionTimeText.text = "Total Session Time: " + FormatTime(totalSessionTime);


    }

    string FormatTime(float time){
        
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60); 
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);  

        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public void Proceed(){
        SceneManager.LoadScene("Main Menu");
    }
}
