using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class NonHostPath : MonoBehaviour
{
    public Transform[] WayPoints;
    public bool Looping = true;

    void Start()
    {
        Entities.Instance.SetEntityLoopPath(gameObject, Looping);

        for (int i = 0; i < WayPoints.Length; i++)
        {
            Entities.Instance.AddEntityWaypoint(gameObject, WayPoints[i].position);
        }
    }
}
