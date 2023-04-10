using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeInputKey : MonoBehaviour
{
    private readonly Array allKeys = Enum.GetValues(typeof(KeyCode));
    bool changingInput = false;
    private KeyCode currentKeyCode;

    private void Update()
    {
        if (changingInput)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            GetComponent<Button>().interactable = false;
            if (Input.anyKeyDown)
            {
                foreach (KeyCode pressedKey in allKeys)
                {
                    if (Input.GetKey(pressedKey))
                    {
                        Debug.Log(pressedKey + " is pressed");
                        changingInput = false;
                        EventSystem.current.SetSelectedGameObject(null);
                        GetComponent<Button>().interactable = true;
                        currentKeyCode = pressedKey;
                        GetComponentInChildren<Text>().text = currentKeyCode.ToString();
                        FindObjectOfType<KeybindOption>().SaveKeyChanges();
                        break;
                    }
                }
            }
        }
               
    }

    public void ChangingInput()
    {
        changingInput = true;
    }

    public void SetupKeycodeBtn(KeyCode setupKeyCode)
    {
        GetComponentInChildren<Text>().text = setupKeyCode.ToString();
        GetComponent<ChangeInputKey>().SetBtnKeycode(setupKeyCode);
    }

    public void SetBtnKeycode(KeyCode keycode)
    {
        currentKeyCode = keycode;
    }

    public KeyCode GetBtnKeycode()
    {
        return currentKeyCode;
    }
}
