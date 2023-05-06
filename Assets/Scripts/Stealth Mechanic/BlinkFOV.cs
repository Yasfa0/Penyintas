using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFOV : FieldOfView
{
    protected bool canRotate = true;
    [SerializeField] protected float plusLimit = 270f;
    [SerializeField] protected float plusInactive = 2f;
    [SerializeField] protected float plusWait = 0.5f;
    [SerializeField] protected float minLimit = 90f;
    [SerializeField] protected float minInactive = 4f;
    [SerializeField] protected float minWait = 0.5f;
    protected bool rotateMin = true;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (canRotate && !PauseMenu.Instance.GetIsPaused())
        {
            if (rotateMin)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,minLimit);
            }else if (!rotateMin)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, plusLimit);
            }

            RotateLimiter();
        }

        

    }

    public void RotateLimiter()
    {
        if (!rotateMin && transform.localEulerAngles.z >= plusLimit)
        {
            //rotateMin = true;
            StartCoroutine(PauseRotation(plusInactive, plusWait));
          
        }
        else if (rotateMin && transform.localEulerAngles.z <= minLimit)
        {
            //rotateMin = false;
            StartCoroutine(PauseRotation(minInactive, minWait));
            
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
        //yield return new WaitForSeconds(0.3f);
        canRotate = true;
        limitCounter++;
        rotateMin = !rotateMin;
        Debug.Log("Rotate Limit Counter " + limitCounter);
        if (limitCounter >= limitPerCycle)
        {
            limitCounter = 0;
            cycleCount++;
            Debug.Log("Rotate Cycle Count " + cycleCount);
        }
    }
}
