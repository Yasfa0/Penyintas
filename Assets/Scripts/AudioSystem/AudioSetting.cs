using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    static float dialogueVolume = 1f;
    static float sfxVolume = 1f;
    static float bgmVolume = 1f;

    public float GetDialogueVolume()
    {
        return dialogueVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

}
