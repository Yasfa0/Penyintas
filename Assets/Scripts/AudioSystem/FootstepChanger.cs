using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepChanger : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footstepClip = new List<AudioClip>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<FootstepScripts>().SetupFootstep(footstepClip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<FootstepScripts>().ResetFootstep();
        }
    }
}
