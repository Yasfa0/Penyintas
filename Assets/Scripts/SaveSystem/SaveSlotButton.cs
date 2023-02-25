using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSlotButton : MonoBehaviour
{
    [SerializeField] private Text typeText;
    [SerializeField] private Text sceneText;
    private int slotIndex;
    private SaveData saveData;

    public void SetupSaveSlot(SaveData setupData,int index)
    {
        saveData = setupData;
        slotIndex = index;

        if (saveData.sceneName.Equals(""))
        {
            typeText.text = "";
            sceneText.text = "Empty Slot";
        }
        else
        {
            sceneText.text = saveData.sceneName;
            if (saveData.isQuicksave)
            {
                typeText.text = "Quicksave";
            }
            else
            {
                typeText.text = "Save Data";
            }
        }
    }

    public void SaveData()
    {
        SaveData tempSave = new SaveData();
        tempSave.isQuicksave = false;
        tempSave.sceneName = SceneManager.GetActiveScene().name;
        //Dummy
        Vector3 pos = FindObjectOfType<PlayerCharacter>().transform.position;
        tempSave.posX = pos.x;
        tempSave.posY = pos.y;
        tempSave.health = FindObjectOfType<PlayerCharacter>().GetHealth();
        SaveSystem.SaveGame(tempSave,"save"+slotIndex);

        UpdateSaveSlot();
    }

    public void LoadData()
    {
        saveData = SaveSystem.LoadSave("save" + slotIndex);
        SaveSystem.SetCurrentSaveData(saveData);
        PlayerData.LoadFromSave();
        FindObjectOfType<SceneLoading>().LoadScene(saveData.sceneName);
    }

    public void UpdateSaveSlot()
    {
       saveData = SaveSystem.LoadSave("save" + slotIndex);
        if (saveData.sceneName.Equals(""))
        {
            typeText.text = "";
            sceneText.text = "Empty Slot";
        }
        else
        {
            sceneText.text = saveData.sceneName;
            if (saveData.isQuicksave)
            {
                typeText.text = "Quicksave";
            }
            else
            {
                typeText.text = "Save Data";
            }
        }
    }

}
