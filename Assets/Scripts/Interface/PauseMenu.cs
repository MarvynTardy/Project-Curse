using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    //public GameObject playerHUD;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!gameIsPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void ResumeGame()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        //playerHUD.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        //playerHUD.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        //playerHUD.SetActive(true);
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
