using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGame : MonoBehaviour
{
    public AddButtonsScript addButtonsScript;
    public GameController gameController;
    // Start is called before the first frame update

    //public static bool cardgameActive = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
		{
            addButtonsScript.GameSetup(8);
            gameController.allowedNumberOfMoves = 12;
            gameController.minigameTimer.timeAllowed = 20f;
            gameController.BeginGame();
		}
    }
    
    
}
