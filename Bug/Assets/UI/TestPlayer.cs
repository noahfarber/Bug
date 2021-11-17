using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public int minSuspicion = 0;
    public int currentSuspicion;

    public SuspicionBar suspicionBar;

    // Start is called before the first frame update
    void Start()
    {
        currentSuspicion = minSuspicion;
        suspicionBar.SetMinSuspicion(minSuspicion);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
		{
            IncreaseSuspicion(10);
		}
    }

    void IncreaseSuspicion(int sus)
	{
        if(currentSuspicion < 100)
		{
            currentSuspicion += sus;
        }

        //Debug.Log(currentSuspicion);

        suspicionBar.SetSuspicion(currentSuspicion);
	}
}
