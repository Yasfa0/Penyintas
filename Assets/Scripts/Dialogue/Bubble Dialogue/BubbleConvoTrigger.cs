using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleConvoTrigger : MonoBehaviour, IBubbleTrigger
{
    [SerializeField] private int dialogueIndex;
    [SerializeField] private BubbleDialogueController bubbleController;
    [SerializeField] private int activeBubbleID;
    [SerializeField] private float delay = 0.2f;

    public void TriggerBubble()
    {
        StartCoroutine(StartBubble());
    }

    public IEnumerator StartBubble()
    {
        yield return new WaitForSeconds(delay);
        bubbleController.SetActiveBubble(activeBubbleID);
        Debug.Log("Active Bubble ID " + activeBubbleID);
        bubbleController.UpdateBubbleTalkStatus();
    }

    public int GetDialogueIndex()
    {
        return dialogueIndex;
    }
}
