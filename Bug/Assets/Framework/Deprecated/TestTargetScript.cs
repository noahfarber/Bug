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
        theHost.LinkedObject = gameObject;  //  This will keep the linked object synchronized automatically 
        theHost.Velocity = 1000.0f;  //  Units/second for path moving
        theHost.JumpToLinked();  //  Make the internal location match the linked object's location

        Vector3 ext = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.max;
        float maxDim = System.Math.Max(ext.x, ext.y);
        theHost.Radius = maxDim;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Entered");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision Stay");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision Exit");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            theHost.ClearPath();
            theHost.Location.Move(0, Velocity*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            theHost.ClearPath();
            theHost.Location.Move(0, -Velocity*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            theHost.ClearPath();
            theHost.Location.Move(-Velocity*Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            theHost.ClearPath();
            theHost.Location.Move(Velocity*Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.R))
        {
            theHost.ClearPath();
            theHost.Location.X = 500;
            theHost.Location.Y = 0;
        }
        if (Input.GetKey(KeyCode.U))  //  "Unlink" (toggle link status)
        {
            if (theHost.LinkedObject != null)  //  We are currently linked, so clear the link...
            {
                theHost.LinkedObject = null;
            }
            else  //  Not currently linked, so re-establish the link...
            {
                theHost.LinkedObject = gameObject;
            }
        }
        if (Input.GetMouseButtonDown(0))  //  Mouse is being pressed...
        {
            Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            theHost.ClearPath();
            theHost.AddWaypoint(mPos.x, mPos.y);
        }
        if (Input.GetMouseButtonDown(1))  //  Mouse is being pressed...
        {
            Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            theHost.AddWaypoint(mPos.x, mPos.y);
        }

        //gameObject.transform.position = new Vector3(theHost.Location.X, theHost.Location.Y, theHost.Location.Z);
    }

}
