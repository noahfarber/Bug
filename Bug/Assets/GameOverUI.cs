using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI RetryText;
    public TextMeshProUGUI FinalScore;
    public TextMeshProUGUI HighScore;

    // Start is called before the first frame update
    void OnEnable()
    {
        int score = ScoreManagerSingleton.Instance.GetScore();
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            HighScore.color = Color.yellow;
        }
        else
        {
            HighScore.color = new Color(.75f, .75f, .75f);
        }

        FinalScore.text = "SCORE: " + score;
        HighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore");
    }

    public void SetWin(bool win)
    {
        if (win)
        {
            TitleText.color = Color.yellow;
            TitleText.text = "YOU WIN!!!";
            RetryText.text = "PLAY AGAIN";
        }
        else
        {
            TitleText.color = new Color(.5f, 0f, 0f);
            TitleText.text = "GAME OVER";
            RetryText.text = "RETRY";
        }
    }
}
