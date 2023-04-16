using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ApproachDistract : DistractEffect
{
    [SerializeField] Transform approachTarget;
    [SerializeField] float approachAngle;
    Animator animator;
    bool approaching = false;
    bool reachedTarget = false;
    FOVController fovController;

    private void Start()
    {
        fovController = GetComponent<EnemyController>().GetFOVController();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (approaching && !reachedTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position,approachTarget.position,5 * Time.deltaTime);
            fovController.transform.localEulerAngles = new Vector3(fovController.transform.localEulerAngles.x, fovController.transform.localEulerAngles.y, approachAngle);
            animator.SetFloat("kecepatan", 1f);
        }

        if (approaching && Mathf.Approximately(transform.position.x,approachTarget.position.x))
        {
            reachedTarget = true;
            approaching = false;
            fovController.GetComponent<Light2D>().intensity = 0.1f;
            animator.SetFloat("kecepatan", 0f);

            // animasi check benda jatuh
            if(transform.name == "Prajurit 4")
            {
                animator.SetTrigger("check");
            }
        }
    }

    public override void DistractedAction()
    {
        approaching = true;
        GetComponent<EnemyController>().DisableFOVControl();
        fovController.transform.localEulerAngles = new Vector3(fovController.transform.localEulerAngles.x, fovController.transform.localEulerAngles.y, approachAngle);
    }
}
