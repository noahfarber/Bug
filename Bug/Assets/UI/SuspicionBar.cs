using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMinSuspicion(int suspicion)
	{
        slider.minValue = suspicion;
        slider.value = suspicion;

        fill.color = gradient.Evaluate(0f);
	}

    public void SetSuspicion(int suspicion)
	{
        slider.value = suspicion;
        fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
