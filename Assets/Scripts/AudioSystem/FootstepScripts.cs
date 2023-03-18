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
        /*foreach (AudioClip stepAudio in footstepClip)
        {
            audioSource.Add(AudioManager.Instance.CreateAudioSource(1,settingData.audioVolume[1]));
        }

        for (int i = 0; i < audioSource.Count; i++)
        {
            audioSource[i].clip = footstepClip[i];
        }*/
        audioSource = AudioManager.Instance.CreateSubAudioSource(footstepClip,1);
    }

    public void SetupFootstep(List<AudioClip> newFootsteps)
    {
        //Hilangin semua AudioSource
        /*foreach (AudioSource source in audioSource)
        {
            Destroy(source);
        }
        audioSource.Clear();*/
        Destroy(audioSource[0].gameObject,0.7f);
        audioSource.Clear();

        //Regenerate AudioSource, Clip, save ke list
        footstepClip = newFootsteps;

        settingData = GameSetting.LoadSetting();
        /*foreach (AudioClip stepAudio in footstepClip)
        {
            audioSource.Add(AudioManager.Instance.CreateAudioSource(1,settingData.audioVolume[1]));
        }

        for (int i = 0; i < audioSource.Count; i++)
        {
            audioSource[i].clip = footstepClip[i];
        }*/

        audioSource = AudioManager.Instance.CreateSubAudioSource(footstepClip, 1);
    }

    public void ResetFootstep()
    {
        SetupFootstep(defaultClip);
    }

    public void PlayFootstepAudio()
    {
        //Apabila ada, hapus semua source yang tidak diperlukan
        /*AudioSource[] allSource = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allSource)
        {
            bool isValid = false;
            foreach (AudioSource validSource in audioSource)
            {
                if (source == validSource)
                {
                    isValid = true;
                }
            }
            if (!isValid && !source.isPlaying)
            {
                Destroy(source);
            }
        }*/
        //AudioManager.Instance.PlayAudio(footstepAudio[audioIndex],1);
        if(audioSource[audioIndex])
        {
            settingData = GameSetting.LoadSetting();
            audioSource[audioIndex].volume = settingData.audioVolume[1] * 15f /100f; 
            audioSource[audioIndex].Play();
        }
        if(audioIndex >= footstepClip.Count-1)
        {
            audioIndex = 0;
        }
        else
        {
            audioIndex++;
        }
    }

}
