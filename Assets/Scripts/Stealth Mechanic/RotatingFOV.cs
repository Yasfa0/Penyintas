using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RotatingFOV : FieldOfView
{
    protected bool canRotate = true;
    //private float[] rotateTarget = {270,90};
    //private int rotateIndex;
    private EnemyAnimationManager animationManager;
    [SerializeField] protected float rotateSpeed = 0.5f;
    [SerializeField] protected float plusLimit = 270f;
    [SerializeField] protected float plusInactive = 2f;
    [SerializeField] protected float plusWait = 0.5f;
    [SerializeField] protected float minLimit = 90f;
    [SerializeField] protected float minInactive = 4f;
    [SerializeField] protected float minWait = 0.5f;
    protected bool rotateMin = false;

    protected override void Start()
    {
        if (rotateSpeed < 0)
        {
            rotateMin = true;
        }

        if (GetComponentInParent<EnemyAnimationManager>())
        {
            animationManager = GetComponentInParent<EnemyAnimationManager>();
        }

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (canRotate && !PauseMenu.Instance.GetIsPaused())
        {
            transform.Rotate(0, 0, rotateSpeed);
            if (animationManager)
            {
                animationManager.AnimateEnemy(0);
            }
        }

        RotateLimiter();

    }

    public void RotateLimiter()
    {
        if (!rotateMin && transform.localEulerAngles.z >= plusLimit)
        {
            rotateMin = true;
            StartCoroutine(PauseRotation(plusInactive, plusWait));
            rotateSpeed = -rotateSpeed;
        }
        else if (rotateMin && transform.localEulerAngles.z <= minLimit)
        {
            rotateMin = false;
            StartCoroutine(PauseRotation(minInactive, minWait));
            rotateSpeed = -rotateSpeed;
        }
    }

    public IEnumerator PauseRotation(float offDuration, float waitDuration)
    {
        canRotate = false; 
        yield return new WaitForSeconds(waitDuration);
        canTarget = false;
        lightField.intensity = 0;
        if (animationManager)
        {
            animationManager.AnimateEnemy(1);
        }
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
        limitCounter++;
        Debug.Log("Rotate Limit Counter " + limitCounter);
        if (limitCounter >= limitPerCycle)
        {
            limitCounter = 0;
            cycleCount++;
            Debug.Log("Rotate Cycle Count " + cycleCount);
        }
    }
}
