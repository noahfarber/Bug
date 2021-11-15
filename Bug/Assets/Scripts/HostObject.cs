using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HostObject : MonoBehaviour
{
    public event System.Action<TilemapCollider2D> OnTilemapCollisionEnter;
    public event System.Action<TilemapCollider2D> OnTilemapCollisionExit;

    public void FireTilemapEntered(TilemapCollider2D collider)
    {
        OnTilemapCollisionEnter?.Invoke(collider);
    }

    public void FireTilemapExited(TilemapCollider2D collider)
    {
        OnTilemapCollisionExit?.Invoke(collider);
    }
}
