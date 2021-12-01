using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class SystemInput : SingletonMonoBehaviour<SystemInput>, ISystemInput
{
    public bool IsAlive 
    {
        get
        {
            return true;
        }
    }/*

    private void Awake()
    {
        InputManager.Instance.RegisterSystemInput(this);
    }
*/
    public bool ProcessInput()
    {
        bool rtn = false;  //  Assume that we aren't handling anything on this pass...

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //  This is where we put global handling for pressing Escape -- likely to pop up a system menu which would push itself as the active input object
            Debug.Log("Pressed escape...");
            if (InputManager.Instance.GamePaused)
            {
                InputManager.Instance.UnpauseGame();
            }
            else
            {
                InputManager.Instance.PauseGame();
            }
            rtn = true;
        }

        return rtn;
    }
}
