using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bug;

public class MiniMap : MonoBehaviour
{
    public Image[] Levels;
    private float[] levelMins = new float[5] { 39f, 15f, -19f, -52f, -100f };
    public int curLevel = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Entities.Instance.PlayerHost != null)
        {
            for (int i = 0; i < levelMins.Length; i++)
            {
                if (Entities.Instance.PlayerHost.transform.position.y >= levelMins[i])
                {
                    curLevel = i;
                    break;
                }
            }
        }

        UpdateLevel();
    }

    void UpdateLevel()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].color = i == curLevel ? Color.yellow : Color.white;
        }
    }
}
