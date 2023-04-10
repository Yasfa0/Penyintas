using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidpointFOV : FieldOfView
{
    protected bool canRotate = true;
    //private float[] rotateTarget = {270,90};
    //private int rotateIndex;
    [SerializeField] protected float rotateSpeed = 0.5f;
    [SerializeField] protected float plusLimit = 270f;
    //[SerializeField] protected float plusInactive = 2f;
    //[SerializeField] protected float plusWait = 0.5f;
    [SerializeField] protected float minLimit = 90f;
    //[SerializeField] protected float minInactive = 4f;
    //[SerializeField] protected float minWait = 0.5f;
    protected bool rotateMin = false;

    [SerializeField] protected float midLimit;
    [SerializeField] protected float midInactive = 4f;
    [SerializeField] protected float midWait = 0.5f;
    protected bool midStop = false;


    // Start is called before the first frame update
    protected override void Start()
    {

        if (rotateSpeed < 0)
        {
            rotateMin = true;
        }
        base.Start();
    }

    // Update is called once per frame
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
        if (!rotateMin && transform.localEulerAngles.z >= plusLimit)
        {
            rotateMin = true;
            //StartCoroutine(PauseRotation(plusInactive, plusWait));
            rotateSpeed = -rotateSpeed;
            UpdateCycle();
        }
        else if (rotateMin && transform.localEulerAngles.z <= minLimit)
        {
            rotateMin = false;
            //StartCoroutine(PauseRotation(minInactive, minWait));
            rotateSpeed = -rotateSpeed;
            UpdateCycle();
        }else if (!midStop && Mathf.Approximately(transform.localEulerAngles.z,midLimit))
        {
            midStop = true;
            StartCoroutine(PauseRotation(midInactive, midWait));
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
        /*limitCounter++;
        Debug.Log("Rotate Limit Counter " + limitCounter);
        if (limitCounter >= limitPerCycle)
        {
            limitCounter = 0;
            cycleCount++;
            midStop = false;
            Debug.Log("Rotate Cycle Count " + cycleCount);
        }*/
    }

    public void UpdateCycle()
    {
        limitCounter++;
        Debug.Log("Rotate Limit Counter " + limitCounter);
        if (limitCounter >= limitPerCycle)
        {
            limitCounter = 0;
            cycleCount++;
            midStop = false;
            Debug.Log("Rotate Cycle Count " + cycleCount);
        }
    }
}
