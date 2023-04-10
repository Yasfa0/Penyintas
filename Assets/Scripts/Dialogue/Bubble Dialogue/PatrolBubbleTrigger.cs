using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBubbleTrigger : MonoBehaviour, IBubbleTrigger
{

    [SerializeField] private int patrolIndex;
    [SerializeField] private BubbleDialogueController bubbleController;
    [SerializeField] private int activeBubbleID;

    public void TriggerBubble()
    {
        //Jalanin Bubblenya
        bubbleController.SetActiveBubble(activeBubbleID);
        bubbleController.UpdateBubbleTalkStatus();
        Debug.Log("Bubble Triggered");
    }

    public int GetPatrolIndex()
    {
        return patrolIndex;
    }
}
