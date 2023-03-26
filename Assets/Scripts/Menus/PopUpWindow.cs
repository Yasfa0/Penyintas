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
        if (FindObjectOfType<PlayerCharacter>())
        {
            Debug.Log("Player Movement False");
            FindObjectOfType<PlayerMovement>().SetCanMove(false);
            FindObjectOfType<PlayerMovement>().StopMovement();
        }
        if (FindObjectOfType<PauseMenu>())
        {
            //FindObjectOfType<PauseMenu>().PauseButtonVisibility(false);
            FindObjectOfType<PauseMenu>().SetCanPause(false);
        }
    }

    private void Update()
    {
        if (Time.time - startTime >= inputWait)
        {
            if (Input.anyKeyDown)
            {
                if (FindObjectOfType<PlayerMovement>())
                {
                    Debug.Log("Player Movement True");
                    FindObjectOfType<PlayerMovement>().SetCanMove(true);
                    FindObjectOfType<PlayerMovement>().ResetSpeed();
                }

                if (FindObjectOfType<PauseMenu>())
                {
                    //FindObjectOfType<PauseMenu>().PauseButtonVisibility(true);
                    FindObjectOfType<PauseMenu>().SetCanPause(true);
                }
                Destroy(gameObject);
            }
        }
    }


}
