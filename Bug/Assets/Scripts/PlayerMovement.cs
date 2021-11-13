using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Bug;

public class PlayerMovement : MonoBehaviour
{
    public HostObject Host; 
    public float Speed = 5f;

    private Animator _Animator;
    private Rigidbody2D _Rigidbody;
    private Collider2D _Collider;
    private Vector2 _Movement = Vector2.zero;
    private Player _Player;

    private void Start()
    {
        // Set inital host
        ChangeHost(Host);

        // Initialize Player
        _Player = Player.InitPlayer();
        _Player.SetLabel("MainCharacter");
        Vector3 objPos = gameObject.transform.position;
        _Player.Location.X = objPos.x;
        _Player.Location.Y = objPos.y;
        _Player.Location.Z = objPos.z;
        _Player.OnProcess += DoPlayerProcess;
    }

    private void Update()
    {
        // Inputs
        CheckInput();
        UpdateAnimator();
    }

    private void CheckInput()
    {
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 22.7f;
            Vector2 clickPosition = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);

            // Cast a ray straight down.
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, 100f);

            //Debug.DrawLine(clickPosition, clickPosition + Vector2.up * 100f);
            if (hit.collider != null)
            {
                if(hit.transform.GetComponent<HostObject>() != null)
                {
                    Debug.Log("Clicked on Host: " + hit.transform.name);
                    ChangeHost(hit.transform.GetComponent<HostObject>());
                }
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            _Player.Location.X = 0f;
            _Player.Location.Y = 0f;
        }
    }

    private void DoPlayerProcess(float deltaTime)
    {
        // Movements
        Vector2 newPosition = new Vector3(_Player.Location.X + _Movement.x * deltaTime * Speed, _Player.Location.Y + _Movement.y * deltaTime * Speed, 0f);
        _Player.Location.Move(_Movement.x * deltaTime * Speed, _Movement.y * deltaTime * Speed);
        transform.position = _Player.Location.XYZ();
    }

    private void UpdateAnimator()
    {
        _Animator.SetFloat("Horizontal", _Movement.x);
        _Animator.SetFloat("Vertical", _Movement.y);
        _Animator.SetFloat("Speed", _Movement.sqrMagnitude);
    }

    private void ChangeHost(HostObject host)
    {
        if (host.GetComponent<Animator>()) { _Animator = host.transform.GetComponent<Animator>(); }
        else { Debug.LogError("Host did not have an Animator attached."); }

        if (host.GetComponent<Rigidbody2D>()) { _Rigidbody = host.transform.GetComponent<Rigidbody2D>(); }
        else { Debug.LogError("Host did not have an Rigidbody2D attached."); }

        if (host.GetComponent<BoxCollider2D>()) { _Collider = host.transform.GetComponent<BoxCollider2D>(); }
        else { Debug.LogError("Host did not have a BoxCollider2D attached."); }

        // Clear movements
        _Movement = Vector2.zero;
        UpdateAnimator();

        // Remove parent
        Host.transform.parent = null;
        
        // Set new host
        Host = host;

        // Update parent
        host.transform.parent = transform;
    }
}
