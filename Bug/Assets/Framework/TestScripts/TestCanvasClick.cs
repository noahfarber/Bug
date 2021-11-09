using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestCanvasClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 cPos = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
        Debug.Log($"Canvas Clicked at: ({eventData.position.x}, {eventData.position.y})  == or ==  ({cPos.x}, {cPos.y}, {cPos.z})");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
