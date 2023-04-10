using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpeaker : MonoBehaviour
{
    [SerializeField] public List<DialogueScriptable> dialogueList = new List<DialogueScriptable>();
    [SerializeField] private bool canSetup = false;

    private void Update()
    {
        //Dummy Only
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueController.Instance.GetTalking() && canSetup)
            {
                DialogueController.Instance.SetCurrentSpeaker(this);
                DialogueController.Instance.SetupDialogue(dialogueList);
                Debug.Log("Speak");
            }
        }
    }

    public void ExecuteEvent(int eventIndex)
    {
        List<IDialogueEvent> executedEvents = new List<IDialogueEvent>();

        foreach (IDialogueEvent dialogueEvent in GetComponents<IDialogueEvent>())
        {
            if (dialogueEvent.GetEventId() == eventIndex)
            {
                //dialogueEvent.StartEvent();
                executedEvents.Add(dialogueEvent);
            }
        }

        foreach (IDialogueEvent exEvent in executedEvents)
        {
            exEvent.StartEvent();
        }

    } 

}
