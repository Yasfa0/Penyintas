using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyController : MonoBehaviour
{
    [SerializeField] List<PatrolPoint> patrolPoints;
    [SerializeField] FOVController FOVController;
    Animator animator;
    Rigidbody2D rb;
    int patrolIndex = 0;
    bool reachedPost = false;
    bool canControl = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        FOVController.SetCanControl(false);
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //FOVController.ChangeFoVList(patrolPoints[0]);
    }

    private void Update()
    {
        if (canControl)
        {
            if (!reachedPost)
            {
                //Gerak ke Pos Patrol
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolIndex].patrolPosition.position, 5 * Time.deltaTime);
                FOVController.transform.localEulerAngles = new Vector3(FOVController.transform.localEulerAngles.x, FOVController.transform.localEulerAngles.y, patrolPoints[patrolIndex].patrolAngle);
                animator.SetFloat("kecepatan",1f);


                if (Mathf.Approximately(transform.position.x, patrolPoints[patrolIndex].patrolPosition.position.x))
                {
                    //Pertama kali sampe di Post Patrol
                    FOVController.SetCanControl(true);
                    FOVController.ChangeFoVList(patrolPoints[patrolIndex]);
                    reachedPost = true;
                    Debug.Log("Reached Patrol Point " + patrolPoints[patrolIndex].patrolPosition);
                    Debug.Log("Patrol Point List Count " + patrolPoints.Count);
                    Debug.Log("Patrol Index " + patrolIndex);
                    Debug.Log("Patrol points Indexes " + patrolPoints[patrolIndex].FOVIndexes.Count);
                }
            }
            else if (reachedPost)
            {
                animator.SetFloat("kecepatan", 0f);
                //Setelah sampe dan selesai 1 siklus pengawasan
                if (FOVController.GetCycleCount() >= 1)
                {
                    FOVController.SetCycleCount(0);
                    reachedPost = false;
                    //FOVController.SetCanControl(false);
                    FOVController.StopControlling();
                    patrolIndex++;
                    if (patrolIndex >= patrolPoints.Count)
                    {
                        patrolIndex = 0;
                    }
                    Debug.Log("Patrol Index " + patrolIndex);
                }
            }
        }
    
    }

    public void DisableFOVControl()
    {
        foreach (FieldOfView fov in FOVController.gameObject.GetComponents<FieldOfView>())
        {
            fov.enabled = false;
        }
        FOVController.enabled = false;
        canControl = false;
    }

    public FOVController GetFOVController()
    {
        return FOVController;
    }
}
