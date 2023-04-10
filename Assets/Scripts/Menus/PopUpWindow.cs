using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpWindow : MonoBehaviour
{
    [SerializeField] Text moveRightTxt, moveLeftTxt, jumpTxt, runTxt, crouchTxt, interactTxt, grabTxt;
    [SerializeField] private float inputWait;
    private float startTime;

    private void Awake()
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

        if (KeybindSaveSystem.LoadKeybind() != null)
        {
            moveRightTxt.text = KeybindSaveSystem.LoadKeybind().moveRight.ToString();
            moveLeftTxt.text = KeybindSaveSystem.LoadKeybind().moveLeft.ToString();
            jumpTxt.text = KeybindSaveSystem.LoadKeybind().jump.ToString();
            runTxt.text = KeybindSaveSystem.LoadKeybind().run.ToString();
            crouchTxt.text = KeybindSaveSystem.LoadKeybind().crouch.ToString();
            interactTxt.text = KeybindSaveSystem.LoadKeybind().interact.ToString();
            grabTxt.text = KeybindSaveSystem.LoadKeybind().grab.ToString();
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
