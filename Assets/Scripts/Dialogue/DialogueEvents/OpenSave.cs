using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSave : MonoBehaviour,IDialogueEvent
{
    [SerializeField] private int eventID;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        if (FindObjectOfType<SaveMenu>())
        {
            Debug.Log(Application.persistentDataPath);
            FindObjectOfType<SaveMenu>().SetupSaveMenu();
        }
    }
}
