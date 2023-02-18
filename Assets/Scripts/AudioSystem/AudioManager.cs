using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private SettingData settingData;


    /* Audio Player Index
     * 0 = BGM
     * 1 = SFX
     * 2 = Dialogue
     */
    [SerializeField] private List<GameObject> audioPlayers = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CheckVolume();
    }
    
    public void CheckVolume()
    {
        for (int i = 0; i < audioPlayers.Count; i++)
        {
            audioPlayers[i].GetComponent<AudioSource>().volume = GameSetting.LoadSetting().audioVolume[i];
        }
    }

    public void PlayAudio(AudioClip targetClip,int audioType)
    {
        AudioSource audioSource = audioPlayers[audioType].GetComponent<AudioSource>();
        audioSource.clip = targetClip;
        audioSource.Play();
    }

    public List<GameObject> GetAudioPlayers()
    {
        return audioPlayers;
    }

}
