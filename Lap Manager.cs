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


    void Update()
    {
        playerLapTime += Time.deltaTime;
        lapTimerText.text = FormatLapTime(playerLapTime);

        totalSessionTime += Time.deltaTime;
    
    }

    string FormatLapTime(float time){
        
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60); 
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);  

        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }

    public void UpdateLapCounter(){
        if(playerLaps == 6){
            if(isAIFinished){
                PlayerPrefs.SetInt("Finished", 2);
            }else{
                PlayerPrefs.SetInt("Finished", 1);
            }
            
            activeTrack = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("activeTrack", activeTrack);

            PlayerPrefs.SetFloat("totalSession", totalSessionTime);
            SceneManager.LoadScene("Finish Screen");
        }

        lapCounterText.text = string.Format("Lap {0}/5", playerLaps);
    }

    public void ResetLapTime(){
        float fastestLap = PlayerPrefs.GetFloat("fastLap");
        print(fastestLap);
        if(playerLapTime < fastestLap){
            playerLapTime = Mathf.Round(playerLapTime * 1000) / 1000;
            PlayerPrefs.SetFloat("fastLap", playerLapTime);
        }
        
        playerLapTime = 0;
    }
}
