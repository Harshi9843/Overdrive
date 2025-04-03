using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

public class MainMenuManager : MonoBehaviour
{
    public GameObject namePanelPlay;
    public TMP_InputField enterName;
    private string lastValidText;

    public GameObject namePanelRecord;
    public TMP_InputField enterNameRecord;
    private string lastValidText2;
    
    private string playerName;
    
    

    void Start()
    {
        enterName.characterLimit = 12;
        enterNameRecord.characterLimit = 12;
        enterName.onValueChanged.AddListener(ValidateInput);
        enterNameRecord.onValueChanged.AddListener(ValidateInput2);
    }

    public void Play(){
        namePanelPlay.SetActive(true);
    }

    public void ViewLapRecords(){
        SceneManager.LoadScene("Lap Records");
    }

    public void Exit(){
        Application.Quit();
    }

    public void UpdateName(){
        playerName = enterName.text;
        PlayerPrefs.SetString("playerName", playerName);
        SceneManager.LoadScene("Car Selection Menu");
    }

    public void OpenRecords(){
        namePanelRecord.SetActive(true);
    }

    public void UpdateNameRecords(){
        playerName = enterNameRecord.text;
        PlayerPrefs.SetString("playerName", playerName);
        playerName = PlayerPrefs.GetString("playerName");
        SceneManager.LoadScene("Player Records");
    }

    public void ValidateInput(string text){
        if(Regex.IsMatch(text, @"[^a-zA-Z\s]")){
            enterName.text = lastValidText;
        }else{
            lastValidText = text;
        }
    }
    public void ValidateInput2(string text){
        if(Regex.IsMatch(text, @"[^a-zA-Z\s]")){
            enterNameRecord.text = lastValidText2;
        }else{
            lastValidText2 = text;
        }
    }
}
