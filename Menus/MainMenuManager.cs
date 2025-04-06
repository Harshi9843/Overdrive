using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] GameObject namePanelPlay;
    [SerializeField] TMP_InputField enterName;
    string lastValidText;

    [SerializeField] GameObject namePanelRecord;
    [SerializeField] TMP_InputField enterNameRecord;
    string lastValidText2;
    
    string playerName;

    void Start()
    {
        // Validation for username input 
        enterName.characterLimit = 12;
        enterName.onValueChanged.AddListener(ValidateInput);
        
        // Validation for username input 
        enterNameRecord.characterLimit = 12;
        enterNameRecord.onValueChanged.AddListener(ValidateInput2);
    }
    

    // Function called when Play button is pressed
    public void Play(){
        // Enabling GUI element where player can enter username
        namePanelPlay.SetActive(true);
    }


    // Function called when Lap Records button is pressed
    public void ViewLapRecords(){
        SceneManager.LoadScene("Lap Records");
    }


    // Function called when close button is pressed
    public void Exit(){
        // Quits the game
        Application.Quit();
    }


    // Function is called when username has been entered after pressing Play button
    public void UpdateName(){
        // Getting username
        playerName = enterName.text;

        // Storing username
        PlayerPrefs.SetString("playerName", playerName);
        SceneManager.LoadScene("Car Selection Menu");
    }


    // Function is called when Player Records button is pressed
    public void OpenPlayerRecords(){
        // Allows player to enter username in order to view their records
        namePanelRecord.SetActive(true);
    }

    // Function called when username has been entered after pressing Player Records button
    public void UpdateNameRecords(){
        playerName = enterNameRecord.text;
        PlayerPrefs.SetString("playerName", playerName);
        playerName = PlayerPrefs.GetString("playerName");
        SceneManager.LoadScene("Player Records");
    }


// These functions are used to validate username inputs that are entered after pressing- 
// -the Play button or the Player Records button  
    
    public void ValidateInput(string text){
        
        // This makes sure the username is only made up of letters and spaces
        if(Regex.IsMatch(text, @"[^a-zA-Z\s]")){
            enterName.text = lastValidText;
        }else{
            lastValidText = text;
        }
    }
    
    public void ValidateInput2(string text){
        
        // This makes sure the username is only made up of letters and spaces
        if(Regex.IsMatch(text, @"[^a-zA-Z\s]")){
            enterNameRecord.text = lastValidText2;
        }else{
            lastValidText2 = text;
        }
    }
}
