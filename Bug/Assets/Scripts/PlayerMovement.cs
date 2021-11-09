using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public Rigidbody2D Rigidbody;
    public Animator Animator;

    private Vector2 _Movement = Vector2.zero;

    private Player _Player;

    private void Start()
    {
        _Player = Player.InitPlayer();
        _Player.SetLabel("MainCharacter");
        Vector3 objPos = gameObject.transform.position;

        _Player.Location.X = objPos.x;
        _Player.Location.Y = objPos.y;
        _Player.Location.Z = objPos.z;

        Vector3 ext = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.max;
        float maxDim = System.Math.Max(ext.x, ext.y);
        _Player.Radius = maxDim;

        _Player.OnProcess += DoPlayerProcess;

    }

    private void Update()
    {
        // Inputs
        _Movement.x = Input.GetAxisRaw("Horizontal");
        _Movement.y = Input.GetAxisRaw("Vertical");

        Animator.SetFloat("Horizontal", _Movement.x);
        Animator.SetFloat("Vertical", _Movement.y);
        Animator.SetFloat("Speed", _Movement.sqrMagnitude);

        if (Input.GetKey(KeyCode.R))
        {
            _Player.Location.X = 0f;
            _Player.Location.Y = 0f;
        }
    }

    void DoPlayerProcess(float deltaTime)
    {
        // Movements
        _Player.Location.Move(_Movement.x * deltaTime * Speed, _Movement.y * deltaTime * Speed);
        Rigidbody.position = new Vector2(_Player.Location.X, _Player.Location.Y);
    }
}
