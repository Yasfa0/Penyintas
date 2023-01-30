using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightColorSetter : MonoBehaviour, DayNightInterface
{
    public Gradient gradient;
    public Light2D[] lights;


   public void GetComponent()
    {
        lights = GetComponentsInChildren<Light2D>();
    }

    public void SetParameter(float time)
    {
        foreach (var light in lights)
        {
            light.color = gradient.Evaluate(time);
        }
    }
}
