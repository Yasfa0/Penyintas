using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChangeSceneEnd : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private string sceneName;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        FindObjectOfType<SceneLoading>().LoadScene(sceneName, 0);
    }
}
