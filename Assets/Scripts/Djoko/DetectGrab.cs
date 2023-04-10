using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGrab : MonoBehaviour
{
    public Transform grabableObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Grabable Object")
        {
            grabableObject = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Grabable Object")
        {
            if (grabableObject != null)
                grabableObject.parent = null;

            grabableObject = null;
        }
    }
}
