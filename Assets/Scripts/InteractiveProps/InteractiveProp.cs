using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveProp : MonoBehaviour
{
    protected bool canInteract = false;
    protected int useCounter = 0;
    [SerializeField] protected int useLimit = 1;

    protected virtual void Interact()
    {
        Debug.Log("Execute Interaction");
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            Interact();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
