using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public static float currentTime = 0f;
    float startingTime = 5f;

    [SerializeField] Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0f)
		{
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        }
    }
}
