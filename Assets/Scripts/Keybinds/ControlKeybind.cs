using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlKeybind 
{
    public KeyCode moveRight, moveLeft, jump, run, crouch, interact, grab;

    public ControlKeybind()
    {
        moveRight = KeyCode.D;
        moveLeft = KeyCode.A;
        jump = KeyCode.W;
        run = KeyCode.LeftShift;
        crouch = KeyCode.S;
        interact = KeyCode.F;
        grab = KeyCode.Space;
    }
}
