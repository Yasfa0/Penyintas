using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayFOV : FieldOfView
{
    [SerializeField] protected float stayAngle;
    [SerializeField] protected float stayDuration = 3f;

    protected float lastStay;

    protected override void Start()
    {
        base.Start();
        lastStay = Time.time;
    }

    protected override void Update()
    {
        base.Update();
        if (!PauseMenu.Instance.GetIsPaused())
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, stayAngle);
        }

        if (Time.time - lastStay >= stayDuration)
        {
            lastStay = Time.time;
            limitCounter++;
            Debug.Log("Stay Limit Counter " + limitCounter);
            if (limitCounter >= limitPerCycle)
            {
                limitCounter = 0;
                cycleCount++;
                Debug.Log("Stay Cycle Count " + cycleCount);
            }
        }

    }

    public override void SetCycleCount(int cycleCount)
    {
        this.cycleCount = cycleCount;
        lastStay = Time.time;
    }
}
