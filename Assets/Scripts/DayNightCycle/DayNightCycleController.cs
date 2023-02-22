using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayNightCycleController : MonoBehaviour
{
    [Range(0, 1f)]
    public float time;
    public bool dayCycling;
    private bool nightSet = true;
    public DayNightInterface[] setters;
    [SerializeField] float cycleSpeed = 0.1f;
    public List<AudioClip> ambienceMusic = new List<AudioClip>();

    private void OnEnable()
    {
        time = 0f;
        dayCycling = true;
        GetSetters();
    }

    public void GetSetters()
    {
        setters = GetComponentsInChildren<DayNightInterface>();

        foreach (var setter in setters)
        {
            setter.GetComponent();
        }
    }

    private void Update()
    {
        if (setters.Length > 0)
        {
            foreach (var setter in setters)
            {
                setter.SetParameter(time);
            }
        }

        if (time > 1)
        {
            dayCycling = false;
        }else if (time < 0)
        {
            dayCycling = true;
        }

        if(time >= 0.8 && !nightSet)
        {
            AudioManager.Instance.PlayAudio(ambienceMusic[1],0);
            nightSet = true;
        }
        else if(time <= 0.8 && nightSet)
        {
            AudioManager.Instance.PlayAudio(ambienceMusic[0], 0);
            nightSet = false;
        }

        if (dayCycling)
        {
            time = Mathf.Lerp(time,1.1f, Time.deltaTime * cycleSpeed);
        }else if (!dayCycling)
        {
            time = Mathf.Lerp(time, -0.1f, Time.deltaTime * cycleSpeed);
        }
    }
}
