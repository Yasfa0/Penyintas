using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpeaker : MonoBehaviour
{
    [SerializeField] public List<DialogueScriptable> dialogueList = new List<DialogueScriptable>();

    private void Update()
    {
        //Dummy Only
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueController.Instance.GetTalking())
            {
                DialogueController.Instance.SetCurrentSpeaker(this);
                DialogueController.Instance.SetupDialogue(dialogueList);
                Debug.Log("Speak");
            }
        }
    }

    public void ExecuteEvent(int eventIndex)
    {
        foreach (IDialogueEvent dialogueEvent in GetComponents<IDialogueEvent>())
        {
            if (dialogueEvent.GetEventId() == eventIndex)
            {
                dialogueEvent.StartEvent();
            }
        }
    } 

}
