using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class MaxSecurityDoor : MonoBehaviour
{
    public Door door;
    private bool HasAlerted = false;

    // Start is called before the first frame update
    void Start()
    {
        HasAlerted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(door.IsOpen() && !HasAlerted)
        {
            if(!Entities.Instance.FullAlert)
            {
                Entities.Instance.AlertAll();
                DialogSingleton.Instance.SpawnDialog("SYSTEM", "Someone has unlocked maximum security! \n Everyone be on the lookout!");
                AudioSingleton.Instance.PlayExitTheme();
                SuspicionMeterSingleton.Instance.AddSuspicion(1f - SuspicionMeterSingleton.Instance.Meter.Amount);
                HasAlerted = true;
            }
        }
    }
}
