using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using SimpleJSON; 

public class RecordManager : MonoBehaviour
{
    public Image image;
    public TrackList trackList;
    private int trackPointer;

    public TextMeshProUGUI trackName;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI recordTime;

    private string databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";

    void Awake()
    {
        PlayerPrefs.SetInt("trackPointer", 0);

        trackPointer = PlayerPrefs.GetInt("trackPointer");
        image.sprite = trackList.trackImages[trackPointer];
        
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
            trackName.text = "Shivagumar Circuit";
        }
        else{
            trackName.text = "Vitavkar Streets";

        }
    }

    public void ChooseDB(){
        if(trackPointer == 0){
            GetFastestLap("Track1");
        }else{
            GetFastestLap("Track2");
        }
    }

    public void GetFastestLap(string trackURL)
    {
        databaseURL = $"{databaseURL}/{trackURL}.json";
        StartCoroutine(FetchFastestLap());
    }

    IEnumerator FetchFastestLap()
    {
        UnityWebRequest request = UnityWebRequest.Get(databaseURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) 
        {
            string json = request.downloadHandler.text;

            if (json == "null" || string.IsNullOrEmpty(json))
            {
                Debug.Log("No lap times found."); 
                yield break;
            }
            JSONNode lapTimes = JSON.Parse(json);
            Debug.Log(lapTimes);
            string fastestPlayer = null;
            float fastestTime = float.MaxValue;

            foreach (KeyValuePair<string, JSONNode> entry in lapTimes)
            {
                float lapTime = entry.Value["Time"].AsFloat;

                if (lapTime < fastestTime)
                {
                    fastestTime = lapTime;
                    fastestPlayer = entry.Key;
                }
            }

            if (fastestPlayer != null)
            {
                playerName.text = fastestPlayer;
                // recordTime.text = (Mathf.Round(fastestTime * 100f) / 100f).ToString() + "s";
                recordTime.text = fastestTime.ToString() + "s";
                Debug.Log($"Fastest Lap: {fastestTime}s by {fastestPlayer}");
            }
            else
            {
                Debug.Log("No valid lap times found.");
            }
        }
        else
        {
            Debug.LogError("Error fetching data: " + request.error);
        }

        databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";
    }
}


