using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTimer : MonoBehaviour
{
    public float timeAllowed;

    [SerializeField] Text countdownText;

    // Update is called once per frame
    void Update()
    {
        if (timeAllowed > 0f)
        {
            timeAllowed -= 1 * Time.unscaledDeltaTime;
            countdownText.text = timeAllowed.ToString("0");
        }
    }
}
