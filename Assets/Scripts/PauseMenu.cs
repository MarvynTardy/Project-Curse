using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!gameIsPaused)
            {
                PauseGame();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void ResumeGame()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        SceneManager.LoadScene("MenuPrincipale");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
