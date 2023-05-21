using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEnableObjectEnd : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private List<GameObject> enableObjects = new List<GameObject>();
    [SerializeField] private bool activate = true;
    [SerializeField] private bool fadeToBlack = false;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        if (fadeToBlack && FindObjectOfType<SceneLoading>())
        {
            FindObjectOfType<SceneLoading>().FadeToBlack();
        }
      
        foreach (GameObject enObject in enableObjects)
        {
            enObject.SetActive(activate);
        }
    }
}
