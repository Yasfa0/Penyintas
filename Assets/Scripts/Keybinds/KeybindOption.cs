using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindOption : MonoBehaviour
{
    [SerializeField] GameObject moveRightBtn, moveLeftBtn, jumpBtn, runBtn, crouchBtn, interactBtn, grabBtn;

    public void SetupControlOption()
    {
        KeybindSaveSystem.SetCurrentKeybind(KeybindSaveSystem.LoadKeybind());
        if (KeybindSaveSystem.LoadKeybind() != null)
        {
            moveRightBtn.GetComponent<ChangeInputKey>().SetupKeycodeBtn(KeybindSaveSystem.currentKeybind.moveRight);
            moveLeftBtn.GetComponent<ChangeInputKey>().SetupKeycodeBtn(KeybindSaveSystem.currentKeybind.moveLeft);
            jumpBtn.GetComponent<ChangeInputKey>().SetupKeycodeBtn(KeybindSaveSystem.currentKeybind.jump);
            runBtn.GetComponent<ChangeInputKey>().SetupKeycodeBtn(KeybindSaveSystem.currentKeybind.run);
            crouchBtn.GetComponent<ChangeInputKey>().SetupKeycodeBtn(KeybindSaveSystem.currentKeybind.crouch);
            interactBtn.GetComponent<ChangeInputKey>().SetupKeycodeBtn(KeybindSaveSystem.currentKeybind.interact);
            grabBtn.GetComponent<ChangeInputKey>().SetupKeycodeBtn(KeybindSaveSystem.currentKeybind.grab);
        }
    }

    private void OnEnable()
    {
        SetupControlOption();
    }

    public void SaveKeyChanges()
    {
        ControlKeybind savedKey = new ControlKeybind();
        savedKey.moveLeft = moveLeftBtn.GetComponent<ChangeInputKey>().GetBtnKeycode();
        savedKey.moveRight = moveRightBtn.GetComponent<ChangeInputKey>().GetBtnKeycode();
        savedKey.jump = jumpBtn.GetComponent<ChangeInputKey>().GetBtnKeycode();
        savedKey.run = runBtn.GetComponent<ChangeInputKey>().GetBtnKeycode();
        savedKey.crouch = crouchBtn.GetComponent<ChangeInputKey>().GetBtnKeycode();
        savedKey.interact = interactBtn.GetComponent<ChangeInputKey>().GetBtnKeycode();
        savedKey.grab = grabBtn.GetComponent<ChangeInputKey>().GetBtnKeycode();

        KeybindSaveSystem.SaveKeybind(savedKey);
        KeybindSaveSystem.SetCurrentKeybind(KeybindSaveSystem.LoadKeybind());
    }
}
