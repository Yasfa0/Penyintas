using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogueSpeaker : DialogueSpeaker
{
    public void OpenDialogue(int inputRoute)
    {
        DialogueController.Instance.SetCurrentSpeaker(this);
        //DialogueController.Instance.SetupDialogue(dialogueList);
        DialogueController.Instance.OpenDialogueRoute(dialogueList,inputRoute);
    }
}
