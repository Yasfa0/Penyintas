using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWatcher : MonoBehaviour
{
    [SerializeField] private float rotateLimit = 180;
    [SerializeField] private GameObject fov;
    [SerializeField] private bool flip = false;
    bool minRota = true;
    float plusRotaY = 0;
    float minRotaY = 180;

    private void Start()
    {
        if (flip)
        {
            plusRotaY = 180;
            minRotaY = 0;
        }
    }

    private void Update()
    {
        //Debug.Log(fov.transform.localEulerAngles.z);
        if (fov.transform.localEulerAngles.z > rotateLimit && minRota)
        {
            Debug.Log("Plus Rota");
            transform.localRotation = Quaternion.Euler(0, plusRotaY, 0);
            minRota = false;
        }
        else if (fov.transform.localEulerAngles.z < rotateLimit && !minRota)
        {
            Debug.Log("Min Rota");
            transform.localRotation = Quaternion.Euler(0, minRotaY, 0);
            minRota = true;
        }
    }
}
