using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;
using TMPro;

public class Door : MonoBehaviour
{
    public int MinimumClearance = 0;

    [SerializeField] private TextMeshPro _ClearanceLevelText;
    private bool _Open = false;
    private RegisteredEntity _PlayerTouching;
    private Animator _Animator;

    private void Awake()
    {
        if(GetComponent<Animator>() != null)
        {
            _Animator = GetComponent<Animator>();
        }
        if(_ClearanceLevelText != null)
        {
            _ClearanceLevelText.text = MinimumClearance.ToString();
        }
    }

    private void Update()
    {
        if(_PlayerTouching != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && HasClearance())
            {
                if(_Animator != null)
                {
                    Interact();
                }
            }
        }
    }

    private void Interact()
    {
        _Animator.SetTrigger(_Open ? "Close" : "Open");
        _Open = !_Open; // Flip current state...
        InteractionTextSingleton.Instance.SetText(_Open ? "Close Door\n(E)" : "Open Door\n(E)"); // Update text with current state...
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Entities.Instance.PlayerHost == collision.gameObject)
        {
            if (Entities.Instance.PlayerHost.GetComponent<RegisteredEntity>() != null)
            {
                _PlayerTouching = Entities.Instance.PlayerHost.GetComponent<RegisteredEntity>();
                SetInteractionText();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InteractionTextSingleton.Instance.ClearText();
        _PlayerTouching = null;
    }

    private void SetInteractionText()
    {
        if (!_Open) // If door is closed...
        {
            if (HasClearance())
            {
                InteractionTextSingleton.Instance.SetText("Open Door\n(E)");
            }
            else
            {
                InteractionTextSingleton.Instance.SetText($"Can't open door:\nClearance required: {MinimumClearance}");
            }
        }
        else // If door is open
        {
            InteractionTextSingleton.Instance.SetText("Close Door\n(E)");
        }
    }

    private bool HasClearance()
    {
        bool rtn = false;

        if (_PlayerTouching != null)
        {
            rtn = _PlayerTouching.ClearanceLevel >= MinimumClearance;
        }

        return rtn;
    }
}
