using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioEvent : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private AudioClip audioClip;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        AudioManager.Instance.PlayNewAudio(audioClip, 0, true);
    }
}
