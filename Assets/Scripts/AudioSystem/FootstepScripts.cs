using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScripts : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footstepClip = new List<AudioClip>();
    private List<AudioSource> audioSource = new List<AudioSource>();
    private int audioIndex;
    private List<AudioClip> defaultClip = new List<AudioClip>();

    private SettingData settingData;

    private void Awake()
    {
        defaultClip = footstepClip;
    }

    private void Start()
    {
        //Generate source di audioplayer SFX sebanyak list clip
        settingData = GameSetting.LoadSetting();
        foreach (AudioClip stepAudio in footstepClip)
        {
            audioSource.Add(AudioManager.Instance.CreateAudioSource(1,settingData.audioVolume[1]));
        }

        for (int i = 0; i < audioSource.Count; i++)
        {
            audioSource[i].clip = footstepClip[i];
        }
    }

    public void SetupFootstep(List<AudioClip> newFootsteps)
    {
        //Hilangin semua AudioSource
        foreach (AudioSource source in audioSource)
        {
            Destroy(source);
        }
        audioSource.Clear();

        //Regenerate AudioSource, Clip, save ke list
        footstepClip = newFootsteps;
        settingData = GameSetting.LoadSetting();
        foreach (AudioClip stepAudio in footstepClip)
        {
            audioSource.Add(AudioManager.Instance.CreateAudioSource(1,settingData.audioVolume[1]));
        }

        for (int i = 0; i < audioSource.Count; i++)
        {
            audioSource[i].clip = footstepClip[i];
        }
    }

    public void ResetFootstep()
    {
        SetupFootstep(defaultClip);
    }

    public void PlayFootstepAudio()
    {
        //AudioManager.Instance.PlayAudio(footstepAudio[audioIndex],1);
        if (audioSource[audioIndex])
        {
            audioSource[audioIndex].Play();
        }
        if (audioIndex >= footstepClip.Count-1)
        {
            audioIndex = 0;
        }
        else
        {
            audioIndex++;
        }
    }

}
