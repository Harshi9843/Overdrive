using Unity.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.Rendering;
using System.Collections;
using UnityEngine.Networking;
using SimpleJSON;
using System.Collections.Generic;

public class PlayerRecord : MonoBehaviour
{
    public Image image;
    public TrackList trackList;
    private int trackPointer;

    public TextMeshProUGUI trackNameText;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI recordTimeText;

    public string playerName;

    public float time;

    private string databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";

    void Awake()
    {
        PlayerPrefs.SetInt("trackPointer", 0);

        trackPointer = PlayerPrefs.GetInt("trackPointer");
        image.sprite = trackList.trackImages[trackPointer];
        playerName = PlayerPrefs.GetString("playerName");

        ChooseDB();
    }


    public void rightArrow(){
        if(trackPointer < trackList.trackImages.Length - 1){
            trackPointer++;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            image.sprite = trackList.trackImages[trackPointer];
            ChooseDB();
            SetTrackInfo();
        }
    }

    public void leftArrow(){
        if(trackPointer > 0){
            trackPointer--;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            image.sprite = trackList.trackImages[trackPointer];
            ChooseDB();
            SetTrackInfo();
        }
    }


    public void GoBack(){
        SceneManager.LoadScene("Main Menu");
    }

    public void SetTrackInfo(){
        if(trackPointer == 0){
            trackNameText.text = "Shivagumar Circuit";
        }
        else{
            trackNameText.text = "Vitavkar Streets";

        }
    }

    public void ChooseDB(){
        if(trackPointer == 0){
            databaseURL = $"{databaseURL}/Track1";
            GetPlayerTime(playerName);
        }else{
            databaseURL = $"{databaseURL}/Track2";
            GetPlayerTime(playerName);
        }
    }

    public void GetPlayerTime(string playerName)
    {
        StartCoroutine(GetTimeRoutine(playerName));
    }

    private IEnumerator GetTimeRoutine(string playerName)
    {
        string url = databaseURL + "/"  + playerName + ".json";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;

                if(jsonResponse == "null"){
                    recordTimeText.text = "";
                    playerNameText.text = "No Lap has been set!";
                    Debug.Log(playerName + " has no lap on this track!!");
                }else{
                    var jsonNode = JSON.Parse(jsonResponse);
                    time = jsonNode["Time"].AsFloat;
                    Debug.Log(time);

                    playerNameText.text = playerName;
                    Debug.Log(playerName + "'s Fastest Time: " + time);
                    recordTimeText.text = time.ToString() + "s";
                }

                databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";
            }
            else
            {
                Debug.LogError("Error fetching time: " + request.error);
            }
        }
    }
}
