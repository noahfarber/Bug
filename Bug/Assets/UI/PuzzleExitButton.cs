using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleExitButton : MonoBehaviour
{
    public TestGame testGame;
	public AddButtonsScript addButtonsScript;
    // Start is called before the first frame update
    public void ExitPuzzle()
	{
		addButtonsScript.ResetGame();
		testGame.puzzleField.SetActive(false);
		testGame.puzzleExitButton.SetActive(false);
	}
}
