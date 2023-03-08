using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    float fovAngle = 90f;
    float range = 8f;
    private Transform fovPoint;
    private Transform target;
    public LayerMask layerMask;
    private Light2D lightField;

    private bool canTarget = true;

    private bool canRotate = true;
    private float pauseDuration = 2f;
    private float[] rotateTarget = {270,90};
    private int rotateIndex;
    [SerializeField] private float rotateSpeed = 5f;

    private void Start()
    {
        fovPoint = transform;
        lightField = GetComponent<Light2D>();

        lightField.pointLightOuterRadius = range;
        lightField.pointLightOuterAngle = fovAngle;
        lightField.pointLightInnerAngle = fovAngle;

        if (FindObjectOfType<PlayerCharacter>())
        {
            target = FindObjectOfType<PlayerCharacter>().GetSightTarget();
        }
    }

    private void Update()
    {
        if (canRotate && !PauseMenu.Instance.GetIsPaused())
        {
            transform.Rotate(0, 0, rotateSpeed);
        }

        if (Mathf.Approximately(transform.localEulerAngles.z,rotateTarget[rotateIndex]))
        {
            StartCoroutine(PauseRotation());
            Debug.Log("Rotate Limit");
            rotateSpeed = -rotateSpeed;
            if (rotateIndex >= rotateTarget.Length-1)
            {
                rotateIndex = 0;
            }
            else
            {
                rotateIndex++;
            }
        }

        

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

    public IEnumerator PauseRotation()
    {
        canRotate = false;
        yield return new WaitForSeconds(0.5f);
        canTarget = false;
        lightField.intensity = 0;
        yield return new WaitForSeconds(pauseDuration);
        canTarget = true;
        lightField.intensity = 1;
        yield return new WaitForSeconds(0.5f);
        canRotate = true;
    }


}
