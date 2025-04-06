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
        SceneManager.LoadScene("Main Menu");
    }


    // Function is called to set a track's name
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
        // Accessing the table of the track displayed on the screen from the database
        if(trackPointer == 0){
            databaseURL = $"{databaseURL}/Track1";
            GetPlayerTime(playerName);
        }else{
            databaseURL = $"{databaseURL}/Track2";
            GetPlayerTime(playerName);
        }
    }


    // Function is called to get the fastest lap time of the player
    public void GetPlayerTime(string playerName)
    {
        // Calls function to get lap time from database
        StartCoroutine(GetTimeRoutine(playerName));
    }

    private IEnumerator GetTimeRoutine(string playerName)
    {
        // Accessing a specific's player data from the database
        string url = databaseURL + "/"  + playerName + ".json";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;

                // Checking if the player has set a lap time on the track being checked or not
                if(jsonResponse == "null"){
                    recordTimeText.text = "";
                    playerNameText.text = "No Lap has been set!";
                    Debug.Log(playerName + " has no lap on this track!!");
                }else{
                    var jsonNode = JSON.Parse(jsonResponse);
                    lapTime = jsonNode["Time"].AsFloat;
                    Debug.Log(lapTime);

                    playerNameText.text = playerName;
                    Debug.Log(playerName + "'s Fastest Time: " + lapTime);         //Displaying lap time if there is one
                    recordTimeText.text = lapTime.ToString() + "s";
                }
                // Reseting URL to database so that data for other tracks can be accessed
                databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";
            }
            else
            {
                Debug.LogError("Error fetching time: " + request.error);
            }
        }
    }
}
