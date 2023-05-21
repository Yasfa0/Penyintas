using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarationEventKeySpeaker : NarationSpeaker
{
    [SerializeField] private int checkedEventKey;
    private bool dialogueTriggered = false;

    private void Update()
    {
        if (SaveSystem.currentSaveData.eventKey[checkedEventKey] && !dialogueTriggered)
        {
            NarationController.Instance.SetCurrentSpeaker(this);
            NarationController.Instance.SetupDialogue(dialogueList);
            Debug.Log("Narrate");
            dialogueTriggered = true;
        }
    }

    public void SetDialogueTrigger(bool val)
    {
        dialogueTriggered = val;
    }
}
