using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpedUpPCEnd : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private float maxSpeed = 1f;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        FindObjectOfType<PlayerMovement>().SetKecepatanMaks(maxSpeed);
    }
}
