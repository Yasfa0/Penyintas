using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventKeySpawnCheck : MonoBehaviour
{
   [SerializeField] private int eventKeyIndex;
   [SerializeField] private bool passVisibility;

    private void Start()
    {
        //if (SaveSystem.currentSaveData.eventKey[eventKeyIndex])
        if(SaveSystem.currentSaveData != null && SaveSystem.currentSaveData.eventKey[eventKeyIndex])
        {
            gameObject.SetActive(passVisibility);
        }
    }
}