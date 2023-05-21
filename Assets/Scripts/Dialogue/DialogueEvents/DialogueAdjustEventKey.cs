using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAdjustEventKey : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] int eventKeyIndex;
    [SerializeField] bool keyValue;


    [SerializeField] private int forwardCheckID = -2;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        if (SaveSystem.currentSaveData != null)
        {
            SaveSystem.currentSaveData.eventKey[eventKeyIndex] = keyValue;
            Debug.Log("Event Key " + eventKeyIndex + " is now " + SaveSystem.currentSaveData.eventKey[eventKeyIndex]);
        }

        foreach (DialogueKeyCheckEnableEnd forwardCheck in GetComponents<DialogueKeyCheckEnableEnd>())
        {
            if (forwardCheck.GetEventId() == forwardCheckID)
            {
                forwardCheck.StartEvent();
            }
        }
    }
}
