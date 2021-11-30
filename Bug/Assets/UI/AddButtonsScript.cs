using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtonsScript : MonoBehaviour
{
	[SerializeField]
	private Transform puzzlefield;

	[SerializeField]
	private GameObject btn;

	public GameController gameController;
	//need to call GameSetup from other code and set number of cards

	public void GameSetup(int numberOfCards)
	{
		//set up the buttons through instantation
		for (int i = 0; i < numberOfCards; i++)
		{
			GameObject button = Instantiate(btn);
			//name our buttons
			button.name = "" + i;
			button.tag = "PuzzleCard";
			button.transform.SetParent(puzzlefield, false);
		}
	}

	public void ResetGame()
	{
		GameObject[] cards = GameObject.FindGameObjectsWithTag("PuzzleCard");
		int length = cards.Length;
		foreach (GameObject card in cards)
		{
			GameObject.Destroy(card);
		}
		for(int i = 0; i < length; i++)
		{
			gameController.btns.RemoveAt(0);
		}
	}
}
