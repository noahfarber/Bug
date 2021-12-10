using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;


public class TestPlayerMovement : MonoBehaviour, IManagedInput
{
    public float Speed = 8.0f;

    private void Awake()
    {
        Entities.Instance.PlayerCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SystemInput.Instance.IsAlive)
        {
            //  Just reference the above property to ensure the singleton is around...
        }
        InputManager.Instance.ActivateInput(this);
        Entities.Instance.JumpToNearest(transform.position);
        AudioSingleton.Instance.PlayMainTheme();
    }

    private float timeSinceLastJump = 0f;
    public void ProcessInput()
    {
        timeSinceLastJump += Time.deltaTime;
/*
        if (Input.GetKeyDown(KeyCode.H))  //  Switch hosts...
        {
            Entities.Instance.JumpToNearest(Entities.Instance.PlayerHost);
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            //            Entities.Instance.JumpToNearest(clickPosition);
            GameObject obj = Entities.Instance.FindHostAtPoint(clickPosition, true);
            if (obj != null)
            {
                Entities.Instance.PlayerHost = obj;
                if(timeSinceLastJump > 5f)
                {
                    SuspicionMeterSingleton.Instance.AddSuspicion(-.1f);
                    timeSinceLastJump = 0f;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 clickPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Entities.Instance.SetPlayerDestination(clickPosition);
        }


        if (Input.mouseScrollDelta.y < 0)
        {
            Entities.Instance.ZoomOutCamera();
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            Entities.Instance.ZoomInCamera();
        }

        float curX = 0;
        float curY = 0;
        if (Input.GetKey(KeyCode.A))
        {
            Entities.Instance.ClearEntityPath(Entities.Instance.PlayerHost);
            curX -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Entities.Instance.ClearEntityPath(Entities.Instance.PlayerHost);
            curX += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Entities.Instance.ClearEntityPath(Entities.Instance.PlayerHost);
            curY += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Entities.Instance.ClearEntityPath(Entities.Instance.PlayerHost);
            curY -= 1;
        }
        if (Input.GetKey(KeyCode.R))
        {
            Entities.Instance.ClearEntityPath(Entities.Instance.PlayerHost);
            curX = 0;
            curY = 0;
            Entities.Instance.SetPlayerPosition(0, 0);
        }

        if ((curX != 0) || (curY != 0))
        {
            Entities.Instance.SetPlayerDestinationOffset(curX*Time.deltaTime*Speed, curY*Time.deltaTime*Speed);
        }
        /*
                if (Input.GetKey(KeyCode.Space))
                {
                    TestStackedInput.Instance.Activate();
                }*/
        /*
                if (Input.GetKeyDown(KeyCode.KeypadEnter))  //  Test key for alert all
                {
                    if(Entities.Instance.FullAlert)
                    {
                        Entities.Instance.ClearAlerts();
                    }
                    else
                    {
                        Entities.Instance.AlertAll();
                    }

                }
        */
        if (!Entities.Instance.IsPlayerInSight())
        {
            SuspicionMeterSingleton.Instance.AddSuspicion(Time.deltaTime / -50);
        }

        if(Entities.Instance.PlayerHost != null && Entities.Instance.PlayerHost.GetComponent<RegisteredEntity>() != null)
        {
            if (Entities.Instance.PlayerHost.GetComponent<RegisteredEntity>().HostType == HostCharactersType.Bug)
            {
                SuspicionMeterSingleton.Instance.AddSuspicion(1f);
            }
        }
    } 

    List<GameObject> restoreList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        List<GameObject> visList = new List<GameObject>();
        Entities.Instance.ProcessAllMovement(Time.deltaTime);
        int visCount = Entities.Instance.PlayerVisibleList(visList);
        if (visCount > 0)
        {
            foreach (GameObject o in visList)
            {
                SpriteRenderer sr = o.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
                    if (!restoreList.Contains(o))
                    {
                        restoreList.Add(o);
                    }
                }
            }
        }
        for (int i=restoreList.Count-1; i>=0; i--)
        {
            if (!visList.Contains(restoreList[i]))  //  Need to restore this sprite's color...
            {
                SpriteRenderer sr = restoreList[i].GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
                }
                restoreList.RemoveAt(i);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning("Collision Enter");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.LogWarning("Collision Exit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Player Trigger Enter");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.LogWarning("Player Trigger Exit");
    }

    public void OnWallCollision(string msg)
    {
        Debug.LogWarning($"Player received wall collision message: [{msg}]");
    }
}
