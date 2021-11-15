using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapWallCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("!!! WALL COLLISION ENTERED !!!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("!!! WALL COLLISION EXITED !!!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"$$$ WALL TRIGGER ENTERED $$$   Collision Name: {collision.name}");
        TestPlayerMovement tpm = collision.gameObject.GetComponent<TestPlayerMovement>();
        if (tpm != null)
        {
            Debug.Log("   ======>>  Found Player Movement Script!");
            tpm.OnWallCollision("My Test Message!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("$$$ WALL TRIGGER EXITED $$$");
    }
}
