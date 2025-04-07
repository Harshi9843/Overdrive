using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] GameObject pausePanel;
    bool isPaused = false;

    // Function is called once every frame
    void Update()
    {
        // Opening and closing the pause menu when the player presses the ESCAPE key
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
            
        }
    }

    // Function is called when the ESCAPE key is pressed
    void PauseGame(){
        // Freezes the game and opens the pause menu
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;
    }

    // Function is called when the ESCAPE key is pressed while the pause menu is open
    void ResumeGame(){
        // Closes the pause menu and unfreeze the game
        pausePanel.SetActive(false); 
        Time.timeScale = 1;
        isPaused = false;
    }

    // Function is called if the Restart button is presses in th pause menu
    public void Restart(){
        // Closes the pause menu, unfreezes the menu, and restarts the current session
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Function is called when the exit button is pressed on the pause menu
    public void ExitGame(){
        // Closes the pause menu, unfreezes the game, and loads the main menu, exiting the current session
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
