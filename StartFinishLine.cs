using UnityEngine;

public class StartFinishLine : MonoBehaviour
{
    public LapManager lapManager;

    private void OnTriggerEnter(Collider other)
    {
        PlayerCarInput playerCar = other.GetComponent<PlayerCarInput>();
        AICarController AIcar = other.GetComponent<AICarController>();

        if(playerCar){
            lapManager.playerLaps++;
            lapManager.ResetLapTime();
            lapManager.UpdateLapCounter();
        }

        if(AIcar){
            lapManager.AILaps++;

            if(lapManager.AILaps == 6){
                lapManager.isAIFinished = true;
            }
        }
    }
}
