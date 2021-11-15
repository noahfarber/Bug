using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TrackingCamera : MonoBehaviour
{
    private void Awake()
    {
        Entities.Instance.PlayerCam = gameObject.GetComponent<Camera>();
    }

}
