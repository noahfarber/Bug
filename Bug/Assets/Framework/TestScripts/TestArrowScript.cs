using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestArrowScript : MonoBehaviour
{
    Player thePlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = Player.InitPlayer();
        thePlayer.SetLabel("Arrow");
        Vector3 objPos = gameObject.transform.position;

        thePlayer.Location.X = objPos.x;
        thePlayer.Location.Y = objPos.y;
        thePlayer.Location.Z = objPos.z;
    }

    // Update is called once per frame
    void Update()
    {
        bool moved = false;
        if (Input.GetKey(KeyCode.I))
        {
            thePlayer.Location.Move(0, 1);
            moved = true;
        }
        if (Input.GetKey(KeyCode.K))
        {
            thePlayer.Location.Move(0, -1);
            moved = true;
        }
        if (Input.GetKey(KeyCode.J))
        {
            thePlayer.Location.Move(-1, 0);
            moved = true;
        }
        if (Input.GetKey(KeyCode.L))
        {
            thePlayer.Location.Move(1, 0);
            moved = true;
        }

        if (moved)
        {
            gameObject.transform.position = new Vector3(thePlayer.Location.X, thePlayer.Location.Y, thePlayer.Location.Z);
        }

        Host theHost = EntityList.Instance.Find("TargetHost") as Host;
        if ((theHost != null) && (thePlayer != null))
        {
            float targAngle = thePlayer.Location.RotationTo(theHost.Location);
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, targAngle);
            UpdateInfo(theHost);
        }
    }
    void UpdateInfo(Host theHost)
    {
        string msg = $"Player '{thePlayer.Label}' at loc ({thePlayer.Location.X,8:N2}, {thePlayer.Location.Y,8:N2}, {thePlayer.Location.Z,8:N2})  ";
        msg += $"Host '{theHost.Label}' at loc ({theHost.Location.X,8:N2}, {theHost.Location.Y,8:N2}, {theHost.Location.Z,8:N2})   ";
        msg += $"Distance = {thePlayer.Location.DistanceTo(theHost.Location),10:N2}   ";
        msg += $"Facing Rotation = {thePlayer.Location.RotationTo(theHost.Location),10:N2}  Altitude = {thePlayer.Location.AltitudeTo(theHost.Location),10:N2}";
        Debug.Log(msg);
    }

}
