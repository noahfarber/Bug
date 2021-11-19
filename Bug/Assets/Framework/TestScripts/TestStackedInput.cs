using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestStackedInput : Singleton<TestStackedInput>, IManagedInput
{
    private int _lastTickCount = 0;
    public void Activate()
    {
        Debug.Log("Activating Stacked Input!");
        InputManager.Instance.ActivateInput(this);
    }

    private void Deactivate()
    {
        Debug.Log("Deactivating Stacked Input!");
        InputManager.Instance.DeactivateInput(this);
    }

    public void ProcessInput()
    {
        if (Environment.TickCount > (_lastTickCount + 1000))
        {
            Debug.Log($"Processing Stacked Input!  {Environment.TickCount:N0}");
            _lastTickCount = Environment.TickCount;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Deactivate();
        }
    }
}
