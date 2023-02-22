using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    public string sceneName;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        SceneManager.LoadScene(sceneName);
    }
}
