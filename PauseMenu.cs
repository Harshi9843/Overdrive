using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] GameObject pausePanel;
    bool isPaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
            
        }
    }

    void PauseGame(){
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    void ResumeGame(){
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void Restart(){
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame(){
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
