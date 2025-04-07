using UnityEngine;

public static class TimeFormatter
{
    public static string FormatLapTime(float lapTime){
        
        int minutes = Mathf.FloorToInt(lapTime / 60f);
        int seconds = Mathf.FloorToInt(lapTime % 60f);
        int milliseconds = Mathf.FloorToInt(lapTime * 1000f % 1000f);

        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}
