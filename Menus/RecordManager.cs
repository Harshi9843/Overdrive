using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON; 

public class RecordManager : MonoBehaviour
{
    [SerializeField] Image trackImage;
    [SerializeField] TrackList trackList;
    int trackPointer;

    [SerializeField] TextMeshProUGUI trackName;
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI recordTime;

    string trackURL;
    string databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";


    // Function is called before game and other objects are loaded
    void Awake()
    {
        //  Displaying the first track 
        PlayerPrefs.SetInt("trackPointer", 0);
        trackPointer = PlayerPrefs.GetInt("trackPointer");
        trackImage.sprite = trackList.trackImages[trackPointer];
        
        // Calling funntion to display the track record
        ChooseDB();
    }

    
    // Function is called when the right arrow button is pressed
    public void rightArrow(){
        // Displaying the next track if there is one
        if(trackPointer < trackList.trackImages.Length - 1){
            trackPointer++;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            trackImage.sprite = trackList.trackImages[trackPointer];
            ChooseDB();
            SetTrackInfo();
        }
    }


    // Function is called when the left arrow button is pressed
    public void leftArrow(){
        // Displays the preivous track until the initial track is reached
        if(trackPointer > 0){
            trackPointer--;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            trackImage.sprite = trackList.trackImages[trackPointer];
            ChooseDB();
            SetTrackInfo();
        }
    }


    // Function is called when the player presses the exit button
    public void GoBack(){
        // Loading the main menu
        SceneManager.LoadScene("Main Menu");
    }


    // Function is called to set the name of the track displayed
    public void SetTrackInfo(){
        if(trackPointer == 0){
            trackName.text = "Shivagumar Circuit";
        }
        else{
            trackName.text = "Vitavkar Streets";

        }
    }


    // Function is called to access a specific track's table in the database
    public void ChooseDB(){
        if(trackPointer == 0){
            databaseURL = $"{databaseURL}/Track1.json";
        }else{
            databaseURL = $"{databaseURL}/Track2.json";
        }

        StartCoroutine(FetchFastestLap());
    }


    // Function is called to get the fastest lap time set on the track displayed
    IEnumerator FetchFastestLap(){
        // Accessing the database
        UnityWebRequest request = UnityWebRequest.Get(databaseURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success){
            string json = request.downloadHandler.text;

            // Chcking if any lap times have been set on the particular track
            if (json == "null" || string.IsNullOrEmpty(json)){
                Debug.Log("No lap times found."); 
                yield break;
            }
           
            // Determining the fastest lap time by looking at all the players' times
            JSONNode lapTimes = JSON.Parse(json);
            Debug.Log(lapTimes);
            string fastestPlayer = null;
            float fastestTime = float.MaxValue;

            foreach (KeyValuePair<string, JSONNode> entry in lapTimes){
                float lapTime = entry.Value["Time"].AsFloat;

                if (lapTime < fastestTime)
                {
                    fastestTime = lapTime;
                    fastestPlayer = entry.Key;
                }
            }
            
            // Displaying the fastest lap time and the player that set it
            if (fastestPlayer != null){
                playerName.text = fastestPlayer;
                recordTime.text = TimeFormatter.FormatLapTime(fastestTime);
                Debug.Log($"Fastest Lap: {fastestTime}s by {fastestPlayer}");
            }else{
                Debug.Log("No valid lap times found.");
            }
        }
        else{
            Debug.LogError("Error fetching data: " + request.error);
        }

        // Reseting the URL to point to the whole database so other tracks can be accessed 
        databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";
    }
}


