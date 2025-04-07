using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public class LapTimesDB : MonoBehaviour
{
    string databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";

    // Function is called to check if the player set a faster time on the current track than the time stored in database
    public void SubmitLapTime(string playerName, float lapTime, string trackURL)
    {
        // Accessing the track's table in the database
        databaseURL = $"{databaseURL}/{trackURL}";

        // Getting data from the database
        StartCoroutine(CheckAndUpdateLapTime(playerName, lapTime));
    }

    // Function is called to get player data if there is any, and check if the current lap time is faster
    IEnumerator CheckAndUpdateLapTime(string playerName, float newTime)
    {
        // Accessing the player's data
        string playerURL = $"{databaseURL}/{playerName}.json";

        // Accessing the required part of the database
        UnityWebRequest getRequest = UnityWebRequest.Get(playerURL);
        yield return getRequest.SendWebRequest();

        if (getRequest.result == UnityWebRequest.Result.Success)
        {
            string json = getRequest.downloadHandler.text;
            
            if (json == "null")
            {
                // Player does not exist in database
                Debug.Log("New player, adding record...");
                
                // Calling function to add player to database
                StartCoroutine(AddNewLapTime(playerName, newTime));
            }
            else
            {
                // Player exists, checking and updating time
                LapTimeEntry existingEntry = JsonUtility.FromJson<LapTimeEntry>(json);

                // Only updating lap time if it is faster
                if (newTime < existingEntry.Time) 
                {
                    Debug.Log("Updating record with a better lap time!");
                    StartCoroutine(UpdateLapTime(playerName, newTime));
                }
                else
                {
                    Debug.Log("New lap time is not better. No update needed.");
                }
            }

            // Resetting the URL so that other tracks and players can be accessed
            databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";
        }
        else
        {
            Debug.LogError("Error fetching data: " + getRequest.error);
        }
    }

    // Function is called to add a new player and their lap time to the database
    IEnumerator AddNewLapTime(string playerName, float lapTime)
    {
        // Converting lap time to JSON to send to database
        LapTimeEntry entry = new LapTimeEntry(lapTime);
        string json = JsonUtility.ToJson(entry);

        // Adding player to the database
        string playerURL = $"{databaseURL}/{playerName}.json";

        // Sending lap time to database
        UnityWebRequest request = new UnityWebRequest(playerURL, "PUT");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        // Checking to make sure data has been added to database
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("New lap time added!");
        }
        else
        {
            Debug.LogError("Error adding lap time: " + request.error);
        }
    }


    // Function is called to update lap time of player
    IEnumerator UpdateLapTime(string playerName, float newTime)
    {
        // Converting object into JSON to send to database
        LapTimeEntry updatedEntry = new LapTimeEntry(newTime);
        string json = JsonUtility.ToJson(updatedEntry);
        Debug.Log(json);

        // Accessing the player's data
        string playerURL = $"{databaseURL}/{playerName}.json";

        // Sending the lap time to the database
        UnityWebRequest request = new UnityWebRequest(playerURL, "PUT");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        // Checking to ensure data has been recieved by the database
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Lap time updated successfully!");
        }
        else
        {
            Debug.LogError("Error updating lap time: " + request.error);
        }
    }
}


// Serialisable class to convert between C# objects and JSON
[System.Serializable]
public class LapTimeEntry
{
    public float Time;

    public LapTimeEntry(float time)
    {
        Time = time;
    }
}
