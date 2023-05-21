using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarationSpeaker : MonoBehaviour
{
    [SerializeField] public List<DialogueScriptable> dialogueList = new List<DialogueScriptable>();
    [SerializeField] private bool canSetup = false;

    private void Update()
    {
        //Dummy Only
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!NarationController.Instance.GetTalking() && canSetup)
            {
                NarationController.Instance.SetCurrentSpeaker(this);
                NarationController.Instance.SetupDialogue(dialogueList);
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
