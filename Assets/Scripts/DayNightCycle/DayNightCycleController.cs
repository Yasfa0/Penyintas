using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayNightCycleController : MonoBehaviour
{
    [Range(0, 1f)]
    public float time;
    public bool isDay;
    public DayNightInterface[] setters;

    private void OnEnable()
    {
        time = 0f;
        isDay = true;
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
            isDay = false;
        }else if (time < 0)
        {
            isDay = true;
        }

        if (isDay)
        {
            time = Mathf.Lerp(time,1.1f, Time.deltaTime * 0.1f);
        }else if (!isDay)
        {
            time = Mathf.Lerp(time, -0.1f, Time.deltaTime * 0.1f);
        }
    }
}
