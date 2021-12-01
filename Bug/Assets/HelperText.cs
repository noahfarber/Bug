using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperText : MonoBehaviour
{
    List<KeyCode> ExitKeys = new List<KeyCode>(9) { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Space, KeyCode.E, KeyCode.Escape };

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ExitKeys.Count; i++)
        {
            if(Input.GetKeyDown(ExitKeys[i]))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
