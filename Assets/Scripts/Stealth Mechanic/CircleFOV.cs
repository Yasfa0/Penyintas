using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFOV : FieldOfView
{
    protected bool canRotate = true;
    [SerializeField] protected float rotateSpeed = 0.5f;
    [SerializeField] protected float rotateLimit;
    [SerializeField] protected float inactiveDuration = 4f;
    [SerializeField] protected float waitDuration = 0.5f;
    protected bool fullRotation = false;

    protected override void Update()
    {
        base.Update();

        if (canRotate && !PauseMenu.Instance.GetIsPaused())
        {
            transform.Rotate(0, 0, rotateSpeed);
        }

        RotateLimiter();

    }

    public void RotateLimiter()
    {
        if (!fullRotation && Mathf.Approximately(transform.localEulerAngles.z,rotateLimit))
        {
            fullRotation = true;
            StartCoroutine(PauseRotation(inactiveDuration, waitDuration));
        }
    }

    public IEnumerator PauseRotation(float offDuration, float waitDuration)
    {
        canRotate = false;
        yield return new WaitForSeconds(waitDuration);
        canTarget = false;
        lightField.intensity = 0;
        //Debug.Log("Lights Off");
        //Debug.Log("Wait for " + waitDuration);
        if (offDuration > 0)
        {
            yield return new WaitForSeconds(offDuration);
        }
        //Debug.Log("Lights On");
        canTarget = true;
        lightField.intensity = lightIntensity;
        yield return new WaitForSeconds(0.5f);
        canRotate = true;
        fullRotation = false;
        limitCounter++;
        Debug.Log("Circle Limit Counter " + limitCounter);
        if (limitCounter >= limitPerCycle)
        {
            limitCounter = 0;
            cycleCount++;
            Debug.Log("Circle Cycle Count " + cycleCount);
        }
    }

}
