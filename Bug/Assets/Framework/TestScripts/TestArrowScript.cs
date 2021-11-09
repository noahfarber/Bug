using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bug;

public class TestArrowScript : MonoBehaviour
{
    public Text InfoText;
    public RectTransform BoundTransform;
    Player thePlayer = null;

    float xVel = 0.0f;
    float yVel = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = Player.InitPlayer();
        thePlayer.SetLabel("Arrow");
        Vector3 objPos = gameObject.transform.position;

        thePlayer.Location.X = objPos.x;
        thePlayer.Location.Y = objPos.y;
        thePlayer.Location.Z = objPos.z;

        Vector3 ext = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.max;
        float maxDim = System.Math.Max(ext.x, ext.y);
        thePlayer.Radius = maxDim;

        thePlayer.OnProcess += DoPlayerProcess;

    }

    void DoPlayerProcess(float deltaTime)
    {
        thePlayer.Location.Move(xVel * deltaTime, yVel * deltaTime);
        if (BoundTransform != null)
        {
            if ((xVel < 0) && (thePlayer.Location.X < BoundTransform.rect.xMin))
            {
                xVel = -xVel;
            }
            if ((xVel > 0) && (thePlayer.Location.X > BoundTransform.rect.xMax))
            {
                xVel = -xVel;
            }
            if ((yVel < 0) && (thePlayer.Location.Y < BoundTransform.rect.yMin))
            {
                yVel = -yVel;
            }
            if ((yVel > 0) && (thePlayer.Location.Y > BoundTransform.rect.yMax))
            {
                yVel = -yVel;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool moved = false;
        if (Input.GetKey(KeyCode.I))
        {
            yVel += 1.0f;
        }
        if (Input.GetKey(KeyCode.K))
        {
            yVel -= 1.0f;
        }
        if (Input.GetKey(KeyCode.J))
        {
            xVel -= 1.0f;
        }
        if (Input.GetKey(KeyCode.L))
        {
            xVel += 1.0f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            xVel = 0.0f;
            yVel = 0.0f;
        }
        if (Input.GetKey(KeyCode.R))
        {
            xVel = 0.0f;
            yVel = 0.0f;
            thePlayer.Location.X = 0;
            thePlayer.Location.Y = 0;
        }

        gameObject.transform.position = thePlayer.Location.XYZ();

        Host theHost = EntityList.Instance.Find("TargetHost") as Host;
        if ((theHost != null) && (thePlayer != null))
        {
            float targAngle = thePlayer.Location.RotationTo(theHost.Location);
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, targAngle);
        }
        UpdateInfo(theHost);
    }
    void UpdateInfo(Host theHost)
    {
        if (InfoText != null)
        {
            string msg = $"Player '{thePlayer.Label}' @ ({thePlayer.Location.X:N2}, {thePlayer.Location.Y:N2}, {thePlayer.Location.Z:N2}) [{thePlayer.Radius:N2}]  Vel ({xVel:N2}, {yVel:N2})  ";
            msg += $"Host '{theHost.Label}' @ ({theHost.Location.X:N2}, {theHost.Location.Y:N2}, {theHost.Location.Z:N2}) [{theHost.Radius:N2}]  ";
            msg += $"Dist = {thePlayer.Location.DistanceTo(theHost.Location):N2}  ";
            msg += $"Rot = {thePlayer.Location.RotationTo(theHost.Location):N2}  Alt = {thePlayer.Location.AltitudeTo(theHost.Location):N2}  ";
            msg += $"Collided = {((thePlayer.Collided(theHost)) ? "YES" : "no")}";
            InfoText.text = msg;
        }
    }

}
