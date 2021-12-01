using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class GameManager : MonoBehaviour, ISystemInput
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject helpMenuUI;
    public GameObject MainMenu;

    public GameOverUI GameOverScreen;
    public GameObject WinGameScreen;


    public GameObject GamePrefab;
    public GameObject CurrentGameObject;
    public Transform GameRoot;

    private void Awake()
    {
        InputManager.Instance.RegisterSystemInput(this);
        GameManagerSingleton.Instance.Register(this);

        if (CurrentGameObject != null)
        {
            Destroy(CurrentGameObject);
        }
    }

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

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = Time.timeScale == 10f ? 1f : 10f;
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
        GameOverScreen.gameObject.SetActive(false);
        //Time.timeScale = 1f;
    }

    public void Pause()
    {
        InputManager.Instance.PauseGame();
        MainMenu.SetActive(false);
        settingsMenuUI.SetActive(false);
        helpMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        Entities.Instance.ClearAlerts();
        Entities.Instance.Reset();
        if(CurrentGameObject != null)
        {
            Destroy(CurrentGameObject);
        }
        CurrentGameObject = Instantiate(GamePrefab, GameRoot);
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
        Destroy(CurrentGameObject);
        MainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}

public class GameManagerSingleton : Singleton<GameManagerSingleton>
{
    public GameManager GM;

    public void Register(GameManager gm)
    {
        GM = gm;
    }

    public void GameOver(bool win)
    {
        if(GM != null && !GM.GameOverScreen.gameObject.activeSelf)
        {
            InteractionTextSingleton.Instance.ClearText();
            InputManager.Instance.PauseGame();
            Time.timeScale = 0f;

            if (win)
            {
                ScoreManagerSingleton.Instance.ChangeScore(500);
            }

            GM.GameOverScreen.SetWin(win);
            GM.GameOverScreen.gameObject.SetActive(true);

        }
    }
}
