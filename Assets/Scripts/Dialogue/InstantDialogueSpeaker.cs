using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDialogueSpeaker : DialogueSpeaker
{
    private bool dialogueTriggered = false;

    private void Start()
    {
        if (!dialogueTriggered)
        {
            DialogueController.Instance.SetCurrentSpeaker(this);
            DialogueController.Instance.SetupDialogue(dialogueList);
            Debug.Log("Speak");
            dialogueTriggered = true;
        }
    }

    public void SetDialogueTrigger(bool val)
    {
        dialogueTriggered = val;
    }
}
