using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueKeyCheckEnableEnd : MonoBehaviour, IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private bool fadeToBlack = false;

    [SerializeField] private List<int> eventKeys = new List<int>();
    private List<bool> keyCheck = new List<bool>();
    [SerializeField] private List<GameObject> targetObjects = new List<GameObject>();
    [SerializeField] private List<bool> targetVisibility = new List<bool>();

    int passCount = 0;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        for (int i = 0; i < eventKeys.Count; i++)
        {
            //bool tempCheck = false;
            if (SaveSystem.currentSaveData.eventKey[eventKeys[i]])
            {
                passCount++;
                //tempCheck = true;
                //keyCheck.Add(tempCheck);
            }
        }
        Debug.Log(passCount + " True Keys");
        if (passCount >= eventKeys.Count)
        {
            Debug.Log("Check Passed");
            if (fadeToBlack && FindObjectOfType<SceneLoading>())
            {
                FindObjectOfType<SceneLoading>().FadeToBlack();
            }

            /*foreach (GameObject enObject in targetObjects)
            {
                enObject.SetActive(activate);
            }*/

            for (int i = 0; i < targetObjects.Count; i++)
            {
                targetObjects[i].SetActive(targetVisibility[i]);
            }
        }

    }

}
