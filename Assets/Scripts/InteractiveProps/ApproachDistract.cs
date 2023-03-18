using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachDistract : DistractEffect
{
    [SerializeField] Transform approachTarget;
    [SerializeField] float approachAngle;
    bool approaching = false;
    bool reachedTarget = false;
    FOVController fovController;

    private void Start()
    {
        fovController = GetComponent<EnemyController>().GetFOVController();
    }

    private void Update()
    {
        if (approaching && !reachedTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position,approachTarget.position,8 * Time.deltaTime);
            fovController.transform.localEulerAngles = new Vector3(fovController.transform.localEulerAngles.x, fovController.transform.localEulerAngles.y, approachAngle);
        }

        if (Mathf.Approximately(transform.position.x,approachTarget.position.x))
        {
            reachedTarget = true;
            approaching = false;
        }
    }

    public override void DistractedAction()
    {
        approaching = true;
        GetComponent<EnemyController>().DisableFOVControl();
        fovController.transform.localEulerAngles = new Vector3(fovController.transform.localEulerAngles.x, fovController.transform.localEulerAngles.y, approachAngle);
    }
}
