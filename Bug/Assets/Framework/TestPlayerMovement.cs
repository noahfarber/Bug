using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestPlayerMovement : MonoBehaviour
{
    public float Speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        Entities.Instance.JumpToNearest(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))  //  Switch hosts...
        {
            Entities.Instance.JumpToNearest(Entities.Instance.PlayerHost);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
//            Entities.Instance.JumpToNearest(clickPosition);
            GameObject obj = Entities.Instance.FindHostAtPoint(clickPosition, true);
            if (obj != null)
            {
                Entities.Instance.PlayerHost = obj;
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
            Entities.Instance.SetPlayerDestinationOffset(curX, curY);
        }

        Entities.Instance.ProcessAllMovement(Time.deltaTime);
/*        Vector2 _movement = new Vector2(curX, curY) * Speed * Time.deltaTime;
        Entities.Instance.ApplyMovement(Entities.Instance.PlayerHost, _movement);
*/    }

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
