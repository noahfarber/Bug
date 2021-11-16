using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class ClearanceTag : MonoBehaviour
{
    private RegisteredEntity _Host;
    private TextMeshPro _Text;

    void Start()
    {
        _Text = GetComponent<TextMeshPro>();

        if (transform.parent.GetComponent<RegisteredEntity>() != null)
        {
            _Host = transform.parent.GetComponent<RegisteredEntity>();
            _Text.text = _Host.ClearanceLevel.ToString();
        }
        else
        {
            _Text.text = "";
        }
    }
}
