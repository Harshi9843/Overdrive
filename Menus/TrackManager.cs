using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrackManager : MonoBehaviour
{
    [SerializeField] Image trackImage;
    [SerializeField] TrackList trackList;
    int trackPointer;

    [SerializeField] TextMeshProUGUI trackName;
    [SerializeField] TextMeshProUGUI trackDistance;
    [SerializeField] TextMeshProUGUI trackSpeed;

    void Awake()
    {
        PlayerPrefs.SetInt("trackPointer", 0);

        trackPointer = PlayerPrefs.GetInt("trackPointer");
        trackImage.sprite = trackList.trackImages[trackPointer];
    }

    public void rightArrow(){
        if(trackPointer < trackList.trackImages.Length - 1){
            trackPointer++;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            trackImage.sprite = trackList.trackImages[trackPointer];
            SetTrackInfo();
        }
    }

    public void leftArrow(){
        if(trackPointer > 0){
            trackPointer--;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            trackImage.sprite = trackList.trackImages[trackPointer];
            SetTrackInfo();
        }
    }


    public void StartGame(){
        if(trackPointer == 0){
            SceneManager.LoadScene("Track1");
        }
        else if(trackPointer == 1){
            SceneManager.LoadScene("Track2");
        }
    }

    public void SetTrackInfo(){
        if(trackPointer == 0){
            trackName.text = "Shivagumar Circuit";
            trackDistance.text = "Track Length: 3.2 miles";
            trackSpeed.text = "Top Speed: 65mph";
        }
        else{
            trackName.text = "Vitavkar Streets";
            trackDistance.text = "Track Length: 4.3 miles";
            trackSpeed.text = "Top Speed: 75mph";
        }
    }
}
