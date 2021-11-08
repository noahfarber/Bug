using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestTargetScript : MonoBehaviour
{
    Host theHost = null;

    // Start is called before the first frame update
    void Start()
    {
        theHost = Host.AddHost();
        theHost.SetLabel("TargetHost");

        Vector3 objPos = gameObject.transform.position;

        theHost.Location.X = objPos.x;
        theHost.Location.Y = objPos.y;
        theHost.Location.Z = objPos.z;
    }

    // Update is called once per frame
    void Update()
    {
        bool moved = false;
        if (Input.GetKey(KeyCode.W))
        {
            theHost.Location.Move(0, 1);
            moved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            theHost.Location.Move(0, -1);
            moved = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            theHost.Location.Move(-1, 0);
            moved = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            theHost.Location.Move(1, 0);
            moved = true;
        }

        if (moved)
        {
            gameObject.transform.position = new Vector3(theHost.Location.X, theHost.Location.Y, theHost.Location.Z);
        }
    }

}
