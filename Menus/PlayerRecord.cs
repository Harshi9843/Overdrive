using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;

public class PlayerRecordManager : MonoBehaviour
{
    [SerializeField] Image trackImage;
    [SerializeField] TrackList trackList;
    int trackPointer;

    [SerializeField] TextMeshProUGUI trackNameText;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI recordTimeText;

    string playerName;
    float lapTime;
    string databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";


    // Function is called before game and other objects are loaded
    void Awake()
    {
        // Displaying the first track
        PlayerPrefs.SetInt("trackPointer", 0);
        trackPointer = PlayerPrefs.GetInt("trackPointer");
        trackImage.sprite = trackList.trackImages[trackPointer];
        playerName = PlayerPrefs.GetString("playerName");

        // Choosing a particular track's table in the database
        ChooseDB();
    }


    // Function is called when the right arrow button is clicked
    public void rightArrow(){
        // Display the next track if there is one
        if(trackPointer < trackList.trackImages.Length - 1){
            trackPointer++;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            trackImage.sprite = trackList.trackImages[trackPointer];
            
            ChooseDB();       // Choosing the particular track's table in database
            SetTrackName();   // Setting the track's name
        }
    }


    // Function is called when the right arrow button is clicked
    public void leftArrow(){
        // Display the next track if there is one
        if(trackPointer > 0){
            trackPointer--;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            trackImage.sprite = trackList.trackImages[trackPointer];
            ChooseDB();       // Choosing the particular track's table from database
            SetTrackName();   // Setting the track's name
        }
    }


    // Function is called when the exit button is clicked
    public void GoBack(){
        // Loading main menu
        SceneManager.LoadScene("Main Menu");
    }


    // Function is called to display track's name
    public void SetTrackName(){
        // Setting the name of the track displayed on the screen
        if(trackPointer == 0){
            trackNameText.text = "Shivagumar Circuit";
        }
        else{
            trackNameText.text = "Vitavkar Streets";

        }
    }


    // Function is called to access a specific track's table from the database
    public void ChooseDB(){
        if(trackPointer == 0){
            databaseURL = $"{databaseURL}/Track1";
        }else{
            databaseURL = $"{databaseURL}/Track2";
        }

        StartCoroutine(GetTimeRoutine(playerName));
    }

    // Function is called to get the fastest time,from the databse, set by the player on the track displayed
    IEnumerator GetTimeRoutine(string playerName){
        // Accessing a specific player's time from the database
        string url = databaseURL + "/"  + playerName + ".json";
        using (UnityWebRequest request = UnityWebRequest.Get(url)){
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success){
                string jsonResponse = request.downloadHandler.text;

                // Checking if the player has set a lap time on the track or not
                if(jsonResponse == "null"){
                    recordTimeText.text = "";
                    playerNameText.text = "No Lap has been set!";
                    Debug.Log(playerName + " has no lap on this track!!");
                }else{
                    // Displaying the fastest lap time set by the player
                    LapTimeEntry existingEntry = JsonUtility.FromJson<LapTimeEntry>(jsonResponse);
                    lapTime = existingEntry.Time;
                    recordTimeText.text = TimeFormatter.FormatLapTime(lapTime);
                    Debug.Log(lapTime);
                    
                    // Displaying the name of the player
                    playerNameText.text = playerName;
                    Debug.Log(playerName + "'s Fastest Time: " + lapTime);     
                }
                // Reseting the URL to point to the whole database so other tracks can be accessed 
                databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";
            }
            else{
                Debug.LogError("Error fetching time: " + request.error);
            }
        }
    }
}
