using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestEntities : MonoBehaviour
{
    Player thePlayer = null;
    Host theHost = null;
    System.Random rng = new System.Random(System.Environment.TickCount);
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = Player.InitPlayer(0, 0, 0);
        theHost = Host.AddHost(0, 0, 0);
        theHost.SetLabel("My Host");

        UpdateInfo();
        sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        thePlayer.Location.Move(0.1, -0.1);
        EntityLocation L = theHost.Location;
        L.Move(rng.Next(-10, 11), rng.Next(-10, 11));
        if (rng.Next(100) < 10)  //  Slight chance to move the host up a level...
        {
            L.Move(0, 0, 1);
        }
        if (sw.ElapsedMilliseconds >= 500)
        {
            UpdateInfo();
            sw.Reset();
            sw.Restart();
        }
    }

    void UpdateInfo()
    {
        string msg = $"Player '{thePlayer.Label}' at loc ({thePlayer.Location.X,8:N2}, {thePlayer.Location.Y,8:N2}, {thePlayer.Location.Z,8:N2})  ";
        msg += $"Host '{theHost.Label}' at loc ({theHost.Location.X,8:N2}, {theHost.Location.Y,8:N2}, {theHost.Location.Z,8:N2})   ";
        msg += $"Distance = {thePlayer.Location.DistanceTo(theHost.Location),10:N2}";
        Debug.Log(msg);
    }
}
