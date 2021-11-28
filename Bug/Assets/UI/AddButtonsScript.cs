using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtonsScript : MonoBehaviour
{
	[SerializeField]
	private Transform puzzlefield;

	[SerializeField]
	private GameObject btn;

    void Awake()
	{
		//set up the buttons through instantation
		for(int i = 0; i < 8; i++)
		{
			GameObject button = Instantiate(btn);
			//name our buttons 0 - 7
			button.name = "" + i;
			button.transform.SetParent(puzzlefield, false);
		}
	}
}
