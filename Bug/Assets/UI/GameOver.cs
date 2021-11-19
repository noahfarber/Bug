using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;

    void Update()
	{
        if (CountdownTimer.currentTime <= 0f)
        {
            gameOverUI.SetActive(true);
        }
        //Debug.Log(CountdownTimer.currentTime);
    }
}
