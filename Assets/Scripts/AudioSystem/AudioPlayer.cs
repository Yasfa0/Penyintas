using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;

    private void Start()
    {
        for (int i = 0; i < clips.Count; i++)
        {
            AudioManager.Instance.PlayLoopingAudio(clips[i],0);
        }
    }
}
