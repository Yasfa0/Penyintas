using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTempSave : MonoBehaviour
{
    private bool saved;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.currentSaveData == null && !saved)
        {
            SaveData tempSave = new SaveData();
            tempSave.isQuicksave = true;
            tempSave.sceneName = SceneManager.GetActiveScene().name;
            //Dummy
            Vector3 pos = FindObjectOfType<PlayerCharacter>().transform.position;
            tempSave.posX = pos.x;
            tempSave.posY = pos.y;
            tempSave.health = FindObjectOfType<PlayerCharacter>().GetHealth();

            if (SaveSystem.currentSaveData != null)
            {
                tempSave.eventKey = SaveSystem.currentSaveData.eventKey;
            }
            else
            {
                tempSave.eventKey = new bool[100];
            }

            SaveSystem.SaveGame(tempSave, "saveTemp");
            SaveSystem.SetCurrentSaveData(tempSave);

            saved = true;
            Debug.Log("Temp Save created");
        }
    }
}
