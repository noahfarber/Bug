using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class TilemapColliderBehaviour : MonoBehaviour
{
    private TilemapCollider2D _TilemapCollider;

    // Start is called before the first frame update
    void Start()
    {
        _TilemapCollider = GetComponent<TilemapCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<HostObject>())
        {
            Debug.Log("Tilemap collided with player.");
            collision.transform.GetComponent<HostObject>().FireTilemapEntered(_TilemapCollider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<HostObject>())
        {
            Debug.LogError("Tilemap collision with player exited.");
            collision.transform.GetComponent<HostObject>().FireTilemapExited(_TilemapCollider);
        }
    }
}
