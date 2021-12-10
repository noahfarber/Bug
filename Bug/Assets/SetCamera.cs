using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    [SerializeField] private Canvas Canvas;
    private void OnEnable()
    {
        Canvas.worldCamera = Camera.main;
    }
}
