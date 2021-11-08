using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestTargetScript : MonoBehaviour
{
    Host theHost = null;
    float Velocity = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        theHost = Host.AddHost();
        theHost.SetLabel("TargetHost");

        Vector3 objPos = gameObject.transform.position;

        theHost.Location.X = objPos.x;
        theHost.Location.Y = objPos.y;
        theHost.Location.Z = objPos.z;

        Vector3 ext = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.max;
        float maxDim = System.Math.Max(ext.x, ext.y);
        theHost.Radius = maxDim;

    }

    // Update is called once per frame
    void Update()
    {
        bool moved = false;
        if (Input.GetKey(KeyCode.W))
        {
            theHost.Location.Move(0, Velocity*Time.deltaTime);
            moved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            theHost.Location.Move(0, -Velocity*Time.deltaTime);
            moved = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            theHost.Location.Move(-Velocity*Time.deltaTime, 0);
            moved = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            theHost.Location.Move(Velocity*Time.deltaTime, 0);
            moved = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            theHost.Location.X = 500;
            theHost.Location.Y = 0;
            moved = true;
        }


        if (moved)
        {
            gameObject.transform.position = new Vector3(theHost.Location.X, theHost.Location.Y, theHost.Location.Z);
        }
    }

}
