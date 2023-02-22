using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDialogueSpeaker : DialogueSpeaker
{
    private bool dialogueTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !dialogueTriggered)
        {
            DialogueController.Instance.SetCurrentSpeaker(this);
            DialogueController.Instance.SetupDialogue(dialogueList);
            Debug.Log("Speak");
            dialogueTriggered = true; 
        }
    }
}
