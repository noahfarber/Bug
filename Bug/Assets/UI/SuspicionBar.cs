using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMinSuspicion(int suspicion)
	{
        slider.minValue = suspicion;
        slider.value = suspicion;
	}

    public void SetSuspicion(int suspicion)
	{
        slider.value = suspicion;
	}

}
