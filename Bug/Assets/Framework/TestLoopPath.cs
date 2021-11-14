using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestLoopPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 startPoint = (Vector2)gameObject.transform.position + (Vector2.right * 5);
        Vector2 endPoint = startPoint + (Vector2.left * 10);
        Entities.Instance.SetEntityLoopPath(gameObject, true);
        Entities.Instance.SetEntityDestination(gameObject, startPoint);
        Entities.Instance.AddEntityWaypoint(gameObject, endPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
