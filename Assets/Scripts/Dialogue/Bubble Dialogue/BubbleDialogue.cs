using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDialogue : MonoBehaviour
{
    [SerializeField] private float offDuration = 4f;
    [SerializeField] private float onDuration = 3f;
    private int bubbleID;
    float lastOn, lastOff;
    GameObject bubbleBox;
    bool isVisible = false;

    [TextArea(3, 10)]
    public List<string> dialogueList;
    private int dialogueIndex = 0;

    private bool canTalk = true;

    private void Awake()
    {
        bubbleBox = GetComponentInChildren<SpriteRenderer>().gameObject;
        lastOff = Time.time;
    }

    private void Update()
    {
        if (canTalk)
        {
            if (!isVisible && Time.time - lastOff >= offDuration)
            {
                lastOn = Time.time;
                bubbleBox.GetComponentInChildren<TextMesh>().text = dialogueList[dialogueIndex];
                TriggerConvo();
                if (dialogueIndex >= dialogueList.Count - 1)
                {
                    dialogueIndex = 0;
                }
                else
                {
                    dialogueIndex++;
                }
                isVisible = true;
                lastOff = Time.time;
            }

            if (isVisible && Time.time - lastOn >= onDuration)
            {
                isVisible = false;
                lastOn = Time.time;
                lastOff = Time.time;
            }

            bubbleBox.SetActive(isVisible);
        }
    }

    public void SetBubbleTalk(bool status)
    {
        canTalk = status;
        isVisible = status;
        lastOff = Time.time;
        lastOn = Time.time;
        dialogueIndex = 0;

        if (status) { TriggerConvo(); }
        
        bubbleBox.SetActive(isVisible);
        //gameObject.SetActive(status);
    }

    /*public bool GetCanTalk()
    {
        return canTalk;
    }

    public void SetCanTalk(bool canTalk)
    {
        this.canTalk = canTalk;
    }*/

    public void SetBubbleID(int bubbleID)
    {
        this.bubbleID = bubbleID;
    }

    public int GetBubbleID()
    {
        return bubbleID;
    }

    public void TriggerConvo()
    {
        if (GetComponent<BubbleConvoTrigger>())
        {
            //Debug.Log("Convo available");
            /*foreach (BubbleConvoTrigger bubble in GetComponents<BubbleConvoTrigger>())
            {
                if (bubble.GetDialogueIndex() == dialogueIndex)
                {
                    bubble.TriggerBubble();
                }
            }*/
            GetComponent<BubbleConvoTrigger>().TriggerBubble();
        }
    }

}
