using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private Sprite bgImage;

	public Sprite[] puzzles;

	public List<Sprite> gamePuzzles = new List<Sprite>();

	public List<Button> btns = new List<Button>();

	public AddButtonsScript addButtonsScript;
	public TestGame testGame;
	public int timeAllowed;
	public int allowedNumberOfMoves;

	private bool firstGuess, secondGuess;

	private int countGuesses;
	private int countCorrectGuesses;
	private int gameGuesses;

	private int firstGuessIndex, secondGuessIndex;

	private string firstGuessPuzzle, secondGuessPuzzle;

    public void BeginGame()
	{
		GetButtons();
		Addlisteners();
		AddGamePuzzles();
		Shuffle(gamePuzzles);
		gameGuesses = gamePuzzles.Count / 2;
	}

	void GetButtons()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleCard");
		for(int i = 0; i < objects.Length; i++)
		{
			btns.Add(objects[i].GetComponent<Button>());
			btns[i].image.sprite = bgImage;
		}
	}

	void AddGamePuzzles()
	{
		int index = 0;

		for(int i = 0; i < btns.Count; i++)
		{
			//we need two of the same symbol for a match
			if(index == btns.Count / 2)
			{
				index = 0;
			}
			gamePuzzles.Add(puzzles[index]);
			index++;

		}
	}

	void Addlisteners()
	{
		foreach (Button btn in btns)
		{
			//add listener to the buttons once they are created
			btn.onClick.AddListener(() => PickACard());
		}
	}

	public void PickACard()
	{
		//string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
	
		if(!firstGuess)
		{
			firstGuess = true;
			//returns a string, so we need to convert to int
			firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

			firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

			btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
		}
		else if(!secondGuess)
		{
			secondGuess = true;
			//returns a string, so we need to convert to int
			secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

			secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

			btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

			countGuesses++;
			//Debug.Log("Guesses: " + countGuesses);
			StartCoroutine(CheckIfPuzzlesMatch());
			
		}
	}

	IEnumerator CheckIfPuzzlesMatch()
	{
		yield return new WaitForSeconds(1f);
		if(firstGuessPuzzle == secondGuessPuzzle)
		{
			//stop the player from being able to interact with the cards
			yield return new WaitForSeconds(0.5f);
			btns[firstGuessIndex].interactable = false;
			btns[secondGuessIndex].interactable = false;

			//remove the card (make it invisible)
			btns[firstGuessIndex].image.color = new Color(0,0,0,0);
			btns[secondGuessIndex].image.color = new Color(0,0,0,0);

			countCorrectGuesses++;

			CheckIfGameFinished();
		}
		else
		{
			//turn the cards "around" again since they did not match
			yield return new WaitForSeconds(0.5f);
			btns[firstGuessIndex].image.sprite = bgImage;
			btns[secondGuessIndex].image.sprite = bgImage;
			CheckIfGameFinished();
		}

		yield return new WaitForSeconds(0.5f);
		firstGuess = secondGuess = false;
	}

	void CheckIfGameFinished()
	{
		if(countCorrectGuesses == gameGuesses)
		{
			Debug.Log("Player has finished the game in " + countGuesses + " guesses." );
			//go back to the main game from here
		}
		else if(countGuesses == allowedNumberOfMoves)
		{
			Debug.Log("Game over!");
			addButtonsScript.ResetGame();
			testGame.puzzleField.SetActive(false);
			testGame.puzzleExitButton.SetActive(false);
		}
		//timer here
		
	}

	void Shuffle(List<Sprite> list)
	{
		for(int i = 0; i < list.Count; i++)
		{
			Sprite temp = list[i];
			int randomIndex = Random.Range(i, list.Count);
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}
	}
}
