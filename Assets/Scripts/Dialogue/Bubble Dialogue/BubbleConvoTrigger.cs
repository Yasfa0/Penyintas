using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleConvoTrigger : MonoBehaviour, IBubbleTrigger
{
    [SerializeField] private int dialogueIndex;
    [SerializeField] private BubbleDialogueController bubbleController;
    [SerializeField] private int activeBubbleID;

    public void TriggerBubble()
    {
        bubbleController.SetActiveBubble(activeBubbleID);
        bubbleController.UpdateBubbleTalkStatus();
        Debug.Log("Bubble Convo Triggered");
    }

    public int GetDialogueIndex()
    {
        return dialogueIndex;
    }
}
