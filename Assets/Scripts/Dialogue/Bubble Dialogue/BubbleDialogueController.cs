using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDialogueController : MonoBehaviour
{
    [SerializeField] private List<BubbleDialogue> dialogueBubbles = new List<BubbleDialogue>();
    [SerializeField] private int activeBubbleID = 0;

    private void Awake()
    {
        for (int i = 0; i < dialogueBubbles.Count; i++)
        {
            dialogueBubbles[i].SetBubbleID(i);
        }
    }

    private void Start()
    {
        UpdateBubbleTalkStatus();
    }

    public void UpdateBubbleTalkStatus()
    {
        foreach (BubbleDialogue bubble in dialogueBubbles)
        {
            if(bubble.GetBubbleID() == activeBubbleID)
            {
                bubble.SetBubbleTalk(true);
            }
            else
            {
                bubble.SetBubbleTalk(false);
            }
        }
    }

    public void SetActiveBubble(int activeBubbleID)
    {
        this.activeBubbleID = activeBubbleID;
    }

}
