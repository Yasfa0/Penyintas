using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueEvent 
{
    public int GetEventId();
    public void StartEvent();
}
