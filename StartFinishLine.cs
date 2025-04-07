using UnityEngine;

public class StartFinishLine : MonoBehaviour
{
    [SerializeField] LapManager lapManager;

    // Function is called when any game objects collides with it/goes into it
    private void OnTriggerEnter(Collider other)
    {
        PlayerCarInput playerCar = other.GetComponent<PlayerCarInput>();
        AICarController AIcar = other.GetComponent<AICarController>();        // Track 1 AI car
        AICarController2 AIcar2 = other.GetComponent<AICarController2>();     // Track 2 AI car

        // Updating player HUD (lap timer, lap counter) when the player crosses the line
        if(playerCar){
            lapManager.playerLaps++;
            lapManager.ResetLapTime();
            lapManager.UpdateLapCounter();
        }

        // Updating AI car's Lap information
        if(AIcar){
            // Incrementing number of laps AI completed
            lapManager.AILaps++;

            // Recording when the AI car has finished
            if(lapManager.AILaps == 6){
                lapManager.isAIFinished = true;
            }
        }
        
        // Updating AI car's Lap information
        if(AIcar2){
            // Incrementing number of laps AI completed
            lapManager.AILaps++;

            // Recording when the AI car has finished
            print(lapManager.AILaps);
            if(lapManager.AILaps == 6){
                lapManager.isAIFinished = true;
            }
        }
    }
}
