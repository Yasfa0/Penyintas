using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCutsceneEnd : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    private CutsceneController cutsceneManager;

    private void Awake()
    {
        cutsceneManager = GetComponent<CutsceneController>();
    }

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        //FindObjectOfType<PlayerMovement>().SetCanMove(true);
        //FindObjectOfType<CameraFollowTarget>().SetCanFollow(true);
        //cutsceneManager.gameObject.SetActive(false);
        cutsceneManager.StopAllTimeline();
        cutsceneManager.EndCutscene();
    }
}
