using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpWindow : MonoBehaviour
{
    [SerializeField] private float inputWait;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime >= inputWait)
        {
            if (Input.anyKeyDown)
            {
                Destroy(gameObject);
            }
        }
    }


}
