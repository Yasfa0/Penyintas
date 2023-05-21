using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioEvent : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private int bgmID = -1;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        if(bgmID < 0)
        {
            AudioManager.Instance.PlayNewAudio(audioClip, 0, true);
        }
        else
        {
            AudioManager.Instance.ReplaceAudioSlot(audioClip,0,1);
        }
    }
}
