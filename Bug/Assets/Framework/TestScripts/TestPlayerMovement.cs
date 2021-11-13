using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public GameObject Host;
    public float Speed = 8.0f;

    private Vector2 _movement;
    private bool _needReset = true;
    private RaycastHit2D[] hits = new RaycastHit2D[3];

    private GameObject h1 = null;
    private GameObject h2 = null;
    private TrackingCamera c = null;

    // Start is called before the first frame update
    void Start()
    {
        if (h1 == null)
        {
            h1 = GameObject.Find("HeroHost");
        }
        if (h2 == null)
        {
            h2 = GameObject.Find("TreeHost");
        }
        if (c == null)
        {
            GameObject go = GameObject.Find("Main Camera");
            if (go != null)
            {
                c = go.GetComponent<TrackingCamera>();
            }
        }
        _movement = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))  //  Switch hosts...
        {
            if (Host == null)
            {
                Host = (h1 != null) ? h1 : (h2 != null) ? h2 : null;
            }
            else if ((Host == h1) && (h2 != null))
            {
                Host = h2;
            }
            else if ((Host == h2) && (h1 != null))
            {
                Host = h1;
            }
            if (c != null)
            {
                c.Following = Host;
            }
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            if (c != null)
            {
                c.ZoomOut();
            }
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            if (c != null)
            {
                c.ZoomIn();
            }
        }
            
        if (Host != null)  //  Only care about keypresses if there's a host to control...
        {
            float curX = 0;
            float curY = 0;
            if (Input.GetKey(KeyCode.A))
            {
                curX -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                curX += 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                curY += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                curY -= 1;
            }
            if (Input.GetKey(KeyCode.R))
            {
                curX = 0;
                curY = 0;
                _needReset = true;
            }
            _movement = new Vector2(curX, curY);
        }
    }

    private void FixedUpdate()
    {
        if ((Host != null) && (Host.GetComponent<Rigidbody2D>() != null))
        {
            Rigidbody2D rb = Host.GetComponent<Rigidbody2D>();
            Vector2 newPos = new Vector2(rb.position.x + (_movement.x * Time.fixedDeltaTime * Speed), rb.position.y + (_movement.y * Time.fixedDeltaTime * Speed));
            if (_needReset)
            {
                newPos = new Vector2(0, 0);
                _needReset = false;
            }

            int numHits = rb.Cast(_movement, hits, (Time.fixedDeltaTime * Speed * _movement.magnitude));
            if (numHits > 0)
            {
                Debug.LogAssertion($"Found collider hits ({numHits})");
                for (int i=0; i<numHits; i++)
                {
                    RaycastHit2D hit = hits[i];
                    Debug.LogWarning($"....  Hit at {hit.point.x}, {hit.point.y}  at fraction {hit.fraction}");
                }
                newPos = rb.position;
            }

            rb.MovePosition(newPos);
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
