using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private Sprite bgImage;


	public List<Button> btns = new List<Button>();

    void Start()
	{
		GetButtons();
		Addlisteners();
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
		string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
		Debug.Log("You are clicking button: " + name);
	}
}
