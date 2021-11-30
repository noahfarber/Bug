using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGame : MonoBehaviour
{
    public AddButtonsScript addButtonsScript;
    public GameController gameController;
    // Start is called before the first frame update

    public static bool cardgameActive = false;
    public GameObject puzzleField;
    public GameObject puzzleExitButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
		{
            //Time.timeScale = 0f;
            puzzleField.SetActive(true);
            puzzleExitButton.SetActive(true);
            addButtonsScript.GameSetup(16);
            gameController.allowedNumberOfMoves = 40;
            gameController.BeginGame();
		}
    }
    
    
}
