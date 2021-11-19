using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

public class Game : MonoBehaviour
{
    void Update()
    {
        Entities.Instance.ProcessAllMovement(Time.deltaTime);
    }
}
