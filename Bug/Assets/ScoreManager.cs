using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int _Score = 0;
    public int Score
    {
        get
        {
            return _Score;
        }
        set
        {
            _Score = value;
            UpdateText();
        }
    }

    public TextMeshProUGUI ScoreText;
    private float Timer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        ScoreManagerSingleton.Instance.RegisterScoreManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= 1f && _Score > 0)
        {
            Score--;
            Timer = 0f;
        }
    }

    private void UpdateText()
    {
        ScoreText.text = "Score: " + _Score.ToString();
    }
}

public class ScoreManagerSingleton : Singleton<ScoreManagerSingleton>
{
    public ScoreManager Manager;

    public void RegisterScoreManager(ScoreManager manager)
    {
        Manager = manager;
        Manager.Score = 1000;
    }

    public int GetScore()
    {
        int rtn = -1;

        if(Manager != null)
        {
            rtn = Manager.Score;
        }

        return rtn;
    }

    public void ChangeScore(int delta)
    {
        if(Manager != null && Manager.Score + delta >= 0)
        {
            Manager.Score += delta;
        }
    }


}
