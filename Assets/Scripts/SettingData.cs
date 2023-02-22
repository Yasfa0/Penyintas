using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    public bool isFullscreen;
    public float[] audioVolume;

    public SettingData()
    {
        audioVolume = new float[3] { 1f, 1f, 1f};
        isFullscreen = true;
    }
}
