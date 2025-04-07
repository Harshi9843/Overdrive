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


    // Function is called before game and other objects are loaded
    void Awake()
    {
        // Displaying the first track
        PlayerPrefs.SetInt("trackPointer", 0);
        trackPointer = PlayerPrefs.GetInt("trackPointer");
        trackImage.sprite = trackList.trackImages[trackPointer];
    }


    // Function is called when the right arrow button is pressed
    public void rightArrow(){
        // Displaying the nezt track if there is one
        if(trackPointer < trackList.trackImages.Length - 1){
            trackPointer++;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            trackImage.sprite = trackList.trackImages[trackPointer];
            SetTrackInfo();
        }
    }


    // Function is called when the left arrow button is pressed
    public void leftArrow(){
        // Displaying the previous track until the first one is reached
        if(trackPointer > 0){
            trackPointer--;
            PlayerPrefs.SetInt("trackPointer", trackPointer);
            trackImage.sprite = trackList.trackImages[trackPointer];
            SetTrackInfo();
        }
    }


    // Function is called when Select button is pressed
    public void StartGame(){
        // Loading track that the player chose
        if(trackPointer == 0){
            SceneManager.LoadScene("Track1");
        }
        else if(trackPointer == 1){
            SceneManager.LoadScene("Track2");
        }
    }
    

    // Function is called to display track's information
    void SetTrackInfo(){
        // Displaying information just about the track that the player is on
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
