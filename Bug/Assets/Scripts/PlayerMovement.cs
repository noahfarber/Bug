using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class PlayerMovement : MonoBehaviour
{
    public HostObject Host; 

    public float Speed = 5f;
    private Animator _Animator;
    private Rigidbody2D _Rigidbody;
    private Vector2 _Movement = Vector2.zero;
    private Player _Player;

    private void Start()
    {
        ChangeHost(Host);

        _Player = Player.InitPlayer();
        _Player.SetLabel("MainCharacter");
        Vector3 objPos = gameObject.transform.position;

        _Player.Location.X = objPos.x;
        _Player.Location.Y = objPos.y;
        _Player.Location.Z = objPos.z;

        /*Vector3 ext = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.max;
        float maxDim = System.Math.Max(ext.x, ext.y);
        _Player.Radius = maxDim;*/

        _Player.OnProcess += DoPlayerProcess;
    }

    private void Update()
    {
        // Inputs
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.y = Input.GetAxisRaw("Vertical");

        CheckClick();

        UpdateAnimator();

        if (Input.GetKey(KeyCode.R))
        {
            _Player.Location.X = 0f;
            _Player.Location.Y = 0f;
        }
    }

    void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray straight down.
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 22.7f;
            Vector3 startPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 startPosV2 = new Vector2(startPos.x, startPos.y);

            //Debug.LogError(mousePos);
            //Debug.LogError(startPos);

            RaycastHit2D hit = Physics2D.Raycast(startPosV2, Vector2.zero, 100f);
            Debug.DrawLine(startPosV2, startPosV2 + Vector2.up * 100f);

            if (hit.collider != null)
            {
                if(hit.transform.GetComponent<HostObject>() != null)
                {
                    Debug.LogError("Clicked on Host: " + hit.transform.name);
                    ChangeHost(hit.transform.GetComponent<HostObject>());
                }
            }
        }
    }

    void DoPlayerProcess(float deltaTime)
    {
        // Movements
        //_Player.Location.Move(_Movement.x * deltaTime * Speed, _Movement.y * deltaTime * Speed);
        //Rigidbody.MovePosition(new Vector2(_Player.Location.X, _Player.Location.Y));
        //Rigidbody.position = new Vector2(_Player.Location.X, _Player.Location.Y);
        _Rigidbody.MovePosition(new Vector2(transform.position.x + _Movement.x * deltaTime * Speed, transform.position.y + _Movement.y * deltaTime * Speed));
    }

    void UpdateAnimator()
    {
        _Animator.SetFloat("Horizontal", _Movement.x);
        _Animator.SetFloat("Vertical", _Movement.y);
        _Animator.SetFloat("Speed", _Movement.sqrMagnitude);
    }

    void ChangeHost(HostObject host)
    {
        if (host.GetComponent<Animator>())
        {
            _Animator = host.transform.GetComponent<Animator>();
        }
        else { Debug.LogError("Host did not have an Animator attached"); }

        if (host.GetComponent<Rigidbody2D>())
        {
            _Rigidbody = host.transform.GetComponent<Rigidbody2D>();
        }
        else { Debug.LogError("Host did not have an Rigidbody2D attached"); }

        // Remove behaviour from old host
        _Movement = Vector2.zero;
        UpdateAnimator();
        Host.OnCollisionEnter -= HostCollision;

        // Set new host
        Host = host;

        // Update parent and behaviour
        transform.parent = host.transform;

        Host.OnCollisionEnter += HostCollision;
    }

    private void HostCollision(Collision2D collision)
    {
        if(collision.transform.GetComponent<HostObject>() != null)
        {
            ChangeHost(collision.transform.GetComponent<HostObject>());
        }
    }
}
