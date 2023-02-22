using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDialogue : DialogueSpeaker
{
    private bool canInteract = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (!DialogueController.Instance.GetTalking())
            {
                DialogueController.Instance.SetCurrentSpeaker(this);
                DialogueController.Instance.SetupDialogue(dialogueList);
                Debug.Log("Speak");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canInteract = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInteract = false;
        }
    }
}
