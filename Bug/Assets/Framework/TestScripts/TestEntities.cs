using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestEntities : MonoBehaviour
{
    Player thePlayer = null;
    Host theHost = null;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = Player.InitPlayer(10, 5, 0);
        theHost = Host.AddHost(100, 10, -3);

        Debug.Log($"Distance from Player to Host: {thePlayer.Location.DistanceTo(theHost.Location):N2}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
