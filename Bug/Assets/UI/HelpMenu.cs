using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    public GameManager pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToMainMenu();
        }
    }

    void GoBackToMainMenu()
    {
        pauseMenu.helpMenuUI.SetActive(false);

        //set the variables to be false so that the game won't resume when trying to go back to the pause menu
        pauseMenu.Pause();
    }

}
