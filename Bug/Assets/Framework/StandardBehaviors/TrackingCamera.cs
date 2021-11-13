using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public GameObject Following;
    public int MinDist = 2;
    public int MaxDist = 20;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Following != null) && (gameObject.transform.parent != Following.transform))  //  We aren't attached to the correct parent, rectify that!
        {
            gameObject.transform.SetParent(Following.transform, false);
        }
    }

    public void ZoomIn()
    {
        if (cam != null)
        {
            cam.orthographicSize = Mathf.Max(MinDist, cam.orthographicSize - 1);
        }
    }

    public void ZoomOut()
    {
        if (cam != null)
        {
            cam.orthographicSize = Mathf.Min(MaxDist, cam.orthographicSize + 1);
        }
    }

}
