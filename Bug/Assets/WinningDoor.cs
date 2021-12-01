using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class WinningDoor : MonoBehaviour
{
    public Door WinDoor;
    private bool HasWon = false;

    // Update is called once per frame
    void Update()
    {
        if(WinDoor.IsOpen() && !HasWon)
        {
            if(Entities.Instance.PlayerHost.GetComponent<RegisteredEntity>().HostType == HostCharactersType.Bug)
            {
                GameManagerSingleton.Instance.GameOver(true);
                HasWon = true;
            }
            else
            {
                DialogSingleton.Instance.SpawnDialog("SYSTEM", "Uh oh! You can't leave until you retrieve your bug body!");
            }
        }
    }
}
