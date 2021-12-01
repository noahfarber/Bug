using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class PauseMenu : MonoBehaviour, ISystemInput
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject helpMenuUI;
    public GameObject MainMenu;
    public GameObject GamePrefab;
    public GameObject GameRoot;

    private bool _GamePaused = false;

    private void Awake()
    {
        InputManager.Instance.RegisterSystemInput(this);
        if(GameRoot != null)
        {
            Destroy(GameRoot);
        }
    }

    // Update is called once per frame
    public bool ProcessInput()
    {
        bool rtn = false;  //  Assume that we aren't handling anything on this pass...
        if(Input.GetKeyDown(KeyCode.Escape) && !MainMenu.activeSelf && !settingsMenuUI.activeSelf && !helpMenuUI.activeSelf)
		{
            if(InputManager.Instance.GamePaused)
			{
                Unpause();
            }
            else
            {
                Pause();
            }
            rtn = true;
		}
        return rtn;
    }

    public void Unpause()
    {
        InputManager.Instance.UnpauseGame();
        MainMenu.SetActive(false);
        settingsMenuUI.SetActive(false);
        helpMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        _GamePaused = false;
    }

    public void Pause()
    {
        InputManager.Instance.PauseGame();
        MainMenu.SetActive(false);
        settingsMenuUI.SetActive(false);
        helpMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        _GamePaused = true;
    }

    public void PlayGame()
    {
        Entities.Instance.Reset();
        GameRoot = Instantiate(GamePrefab, null);
        Unpause();
    }

    public void LoadSettings()
	{
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void LoadHelp()
    {
        pauseMenuUI.SetActive(false);
        helpMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Camera.main.transform.parent = null;
        helpMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Destroy(GameRoot);
        MainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
