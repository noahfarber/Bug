using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostObject : MonoBehaviour
{
    public event System.Action<Collision2D> OnCollisionEnter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(collision);
    }
}
