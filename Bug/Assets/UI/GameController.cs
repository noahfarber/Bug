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

	private bool firstGuess, secondGuess;

	private int countGuesses;
	private int countCorrectGuesses;
	private int gameGuesses;

	private int firstGuessIndex, secondGuessIndex;

	private string firstGuessPuzzle, secondGuessPuzzle;

    void Start()
	{
		GetButtons();
		Addlisteners();
		AddGamePuzzles();
	}

	void GetButtons()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
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

			if(firstGuessPuzzle == secondGuessPuzzle)
			{
				Debug.Log("The puzzles match!");
			}
			else
			{
				Debug.Log("The puzzles do NOT match.");
			}
		}
	}
}
