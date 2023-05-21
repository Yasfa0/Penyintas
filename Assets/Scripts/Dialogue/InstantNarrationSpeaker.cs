using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantNarrationSpeaker : NarationSpeaker
{
    private bool dialogueTriggered = false;

    private void Start()
    {
        Debug.Log(dialogueTriggered);
        if (!dialogueTriggered)
        {
            NarationController.Instance.SetCurrentSpeaker(this);
            NarationController.Instance.SetupDialogue(dialogueList);
            Debug.Log("Speak");
            dialogueTriggered = true;
        }
    }

    public void SetDialogueTrigger(bool val)
    {
        dialogueTriggered = val;
    }
}
