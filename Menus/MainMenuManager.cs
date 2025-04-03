using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

public class MainMenuManager : MonoBehaviour
{
    public GameObject namePanelPlay;
    public GameObject namePanelRecord;
    public TMP_InputField enterName;
    public TMP_InputField enterNameRecord;
    private string playerName;

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
}
