using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueCutsceneEvent : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private int timelineIndex;
    CutsceneController cutsceneController;

    private void Awake()
    {
        cutsceneController = GetComponent<CutsceneController>();
    }

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        //playableTimeline.Play();
        cutsceneController.PlayTimeline(timelineIndex);
    }
}
