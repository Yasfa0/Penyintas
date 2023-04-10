using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChangeEnd : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private DialogueSpeaker targetSpeaker;
    [SerializeField] private List<DialogueScriptable> newDialogues = new List<DialogueScriptable>();

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        if(targetSpeaker is AreaDialogueSpeaker)
        {
            targetSpeaker.GetComponent<AreaDialogueSpeaker>().SetDialogueTrigger(false);
        }
        targetSpeaker.dialogueList = newDialogues;
    }
}
