using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleExitButton : MonoBehaviour
{
    //public TestGame testGame;
	public AddButtonsScript addButtonsScript;
	public GameController gameController;
    // Start is called before the first frame update
    public void ExitPuzzle()
	{
		addButtonsScript.ResetGame();
		gameController.puzzleField.SetActive(false);
		gameController.puzzleExitButton.SetActive(false);
		gameController.minigameTimerText.SetActive(false);
		gameController.minigameActive = false;
	}
}
