using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject playerHUD;
    EventSystem es;
    private PlayerController player;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

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
        if (playerHUD != null)
        {
            playerHUD.SetActive(true);
        }
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        player.m_NextDashTime += 1f;
        // StartCoroutine(HighlightBtn());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        if (playerHUD != null)
        {
            playerHUD.SetActive(false);
        }
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
