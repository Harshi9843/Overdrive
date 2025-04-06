using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public class LapTimesDB : MonoBehaviour
{
    string databaseURL = "https://overdrive-ab49e-default-rtdb.europe-west1.firebasedatabase.app/";

    public void SubmitLapTime(string playerName, float lapTime, string trackURL)
    {
        databaseURL = $"{databaseURL}/{trackURL}";
        print(databaseURL);
        StartCoroutine(CheckAndUpdateLapTime(playerName, lapTime));
    }

    IEnumerator CheckAndUpdateLapTime(string playerName, float newTime)
    {
        string playerURL = $"{databaseURL}/{playerName}.json"; // Player-specific URL

        UnityWebRequest getRequest = UnityWebRequest.Get(playerURL);
        yield return getRequest.SendWebRequest();

        if (getRequest.result == UnityWebRequest.Result.Success)
        {
            string json = getRequest.downloadHandler.text;
            if (json == "null")
            {
                // Player does NOT exist, create a new record
                Debug.Log("New player, adding record...");
                StartCoroutine(AddNewLapTime(playerName, newTime));
            }
            else
            {
                // Player exists, check and update time
                LapTimeEntry existingEntry = JsonUtility.FromJson<LapTimeEntry>(json);

                if (newTime < existingEntry.Time) // Only update if the new time is better
                {
                    Debug.Log("Updating record with a better lap time!");
                    StartCoroutine(UpdateLapTime(playerName, newTime));
                }
                else
                {
                    Debug.Log("New lap time is not better. No update needed.");
                }
            }
        }
        else
        {
            Debug.LogError("Error fetching data: " + getRequest.error);
        }
    }

    IEnumerator AddNewLapTime(string playerName, float lapTime)
    {
        LapTimeEntry entry = new LapTimeEntry(lapTime);
        string json = JsonUtility.ToJson(entry);

        string playerURL = $"{databaseURL}/{playerName}.json";
        UnityWebRequest request = new UnityWebRequest(playerURL, "PUT");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("New lap time added!");
        }
        else
        {
            Debug.LogError("Error adding lap time: " + request.error);
        }
    }

    IEnumerator UpdateLapTime(string playerName, float newTime)
    {
        LapTimeEntry updatedEntry = new LapTimeEntry(newTime);
        string json = JsonUtility.ToJson(updatedEntry);

        string playerURL = $"{databaseURL}/{playerName}.json";
        UnityWebRequest request = new UnityWebRequest(playerURL, "PUT");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

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

[System.Serializable]
public class LapTimeEntry
{
    public float Time;

    public LapTimeEntry(float time)
    {
        Time = time;
    }
}
