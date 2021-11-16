using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InteractionText : MonoBehaviour
{
    private void Awake()
    {
        InteractionTextSingleton.Instance.RegisterText(GetComponent<TextMeshProUGUI>());
    }
}

public class InteractionTextSingleton : Singleton<InteractionTextSingleton>
{
    private TextMeshProUGUI _Text;

    public void RegisterText(TextMeshProUGUI aText)
    {
        _Text = aText;
    }

    public void SetText(string text = "")
    {
        if(_Text != null)
        {
            _Text.text = text;
        }
        else
        {
            Debug.LogWarning("Can't set text since reference is null.");
        }
    }

    public void ClearText()
    {
        if (_Text != null)
        {
            _Text.text = "";
        }
        else
        {
            Debug.LogWarning("Can't set text since reference is null.");
        }
    }
}
