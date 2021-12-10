using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class HostPath : MonoBehaviour
{
    public bool Looping = true;

    private RegisteredEntity Host;
    private Transform[] WayPoints;

    void Start()
    {
        WayPoints = GetComponentsInChildren<Transform>();

        if(transform.parent.GetComponent<RegisteredEntity>() != null && WayPoints.Length > 0)
        {
            Host = transform.parent.GetComponent<RegisteredEntity>();

            Entities.Instance.SetEntityLoopPath(Host.gameObject, Looping);

            for (int i = 0; i < WayPoints.Length; i++)
            {
                Entities.Instance.AddEntityWaypoint(Host.gameObject, WayPoints[i].position, 2.0f);
            }
        }
    }
}
