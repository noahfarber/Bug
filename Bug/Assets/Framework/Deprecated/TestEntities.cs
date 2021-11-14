using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class TestEntities : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EntityList.Instance.ProcessEntities(0.0f);  //  Do initial process for whatever is in the list...
    }

    // Update is called once per frame
    void Update()
    {
        EntityList.Instance.ProcessEntities(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        EntityList.Instance.FixedProcessEntities(Time.fixedDeltaTime);
    }
}
