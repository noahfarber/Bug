using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public static bool inSettingsMenu = false;
    public static bool inHelpMenu = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject helpMenuUI;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && inSettingsMenu == false)
		{
            if(gamePaused)
			{
                Resume();
			}
            else
            {
                Pause();
            }	
		}
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void LoadSettings()
	{
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        //set the variable to be true so that the game won't resume when trying to go back to the pause menu
        inSettingsMenu = true;
    }

    public void LoadHelp()
    {
        pauseMenuUI.SetActive(false);
        helpMenuUI.SetActive(true);
        //set the variable to be true so that the game won't resume when trying to go back to the pause menu
        inHelpMenu = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
