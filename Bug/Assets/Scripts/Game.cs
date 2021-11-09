using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class Game : MonoBehaviour
{
    void Update()
    {
        EntityList.Instance.ProcessEntities(Time.deltaTime);
    }
}
