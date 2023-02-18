using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private SettingData optionSetting;

    [SerializeField] private Slider[] volumeSlider;
    [SerializeField] private Toggle fullscreenToggle;

    public void SetupOption()
    {
        optionSetting = GameSetting.LoadSetting();
        for (int i = 0; i < volumeSlider.Length; i++)
        {
            volumeSlider[i].value = optionSetting.audioVolume[i];
        }
        fullscreenToggle.isOn = optionSetting.isFullscreen;
    }

    public void UpdateFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void UpdateVolume(int volumeIndex)
    {
        AudioManager.Instance.GetAudioPlayers()[volumeIndex].GetComponent<AudioSource>().volume = volumeSlider[volumeIndex].value;
        Debug.Log("Updating Audio");
    }

    public void SaveOption()
    {
        for (int i = 0; i < optionSetting.audioVolume.Length; i++)
        {
            optionSetting.audioVolume[i] = volumeSlider[i].value;
        }
        optionSetting.isFullscreen = fullscreenToggle.isOn;
        GameSetting.SaveSetting(optionSetting);
    }
}
