using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrigger : MonoBehaviour,IBubbleTrigger
{
    [SerializeField] private int triggerID;
    [SerializeField] private BubbleDialogueController bubbleController;
    [SerializeField] private int activeBubbleID;

    public void TriggerBubble()
    {
        //Jalanin Bubblenya
        bubbleController.SetActiveBubble(activeBubbleID);
        bubbleController.UpdateBubbleTalkStatus();
        Debug.Log("Bubble Triggered");
    }

    public int GetTriggerID()
    {
        return triggerID;
    }
}
