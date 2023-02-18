using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    public List<AudioClip> soundClip = new List<AudioClip>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.Instance.PlayAudio(soundClip[0],0);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.Instance.PlayAudio(soundClip[1],0);
        }
    }
}
