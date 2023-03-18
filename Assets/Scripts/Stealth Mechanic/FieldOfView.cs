using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] protected int FOVId;
    [SerializeField] protected int damage = 1;
    protected float fovAngle = 70f;
    [SerializeField] protected float range = 7f;
    protected Transform fovPoint;
    protected Transform target;
    public LayerMask layerMask;
    protected Light2D lightField;
    protected float lightIntensity;

    protected bool canTarget = true;

    protected int cycleCount = 0;
    protected int limitCounter = 0;
    [SerializeField]  protected int limitPerCycle = 2;

    protected bool isActiveFOV = true;

    protected virtual void Start()
    {
        fovPoint = transform;
        lightField = GetComponent<Light2D>();
        lightIntensity = lightField.intensity;
        lightField.pointLightOuterRadius = range;
        lightField.pointLightOuterAngle = fovAngle;
        lightField.pointLightInnerAngle = fovAngle;

        if (FindObjectOfType<PlayerCharacter>())
        {
            target = FindObjectOfType<PlayerCharacter>().GetSightTarget();
        }
    }

    protected virtual void Update()
    {

        if (target && canTarget)
        {
            Vector2 dir = target.position - fovPoint.position;
            float angle = Vector3.Angle(dir, fovPoint.up);
            RaycastHit2D hit = Physics2D.Raycast(fovPoint.position, dir, range, layerMask);
            if (angle < fovAngle / 2)
            {
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player Detected");
                    hit.collider.gameObject.GetComponent<PlayerCharacter>().TakeDamage(damage);
                }
            }

            Debug.DrawRay(fovPoint.position, dir, Color.red);
        }
        
    }

    public void SetIsActiveFOV(bool isActiveFOV)
    {
        this.isActiveFOV = isActiveFOV;
    }
    
    public int GetFOVId()
    {
        return FOVId;
    }

    public int GetCycleCount()
    {
        return cycleCount;
    }

    public virtual void SetCycleCount(int cycleCount)
    {
        this.cycleCount = cycleCount;
    }

}
