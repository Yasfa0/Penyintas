using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSlotButton : MonoBehaviour
{
    [SerializeField] AudioClip saveSFX;
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
            typeText.text = "SLOT " + (index + 1);
            sceneText.text = "EMPTY";
        }
        else
        {
            sceneText.text = saveData.sceneName;
            if (saveData.isQuicksave)
            {
                typeText.text = "AUTOSAVE";
            }
            else
            {
                typeText.text = "SLOT "+(index+1);
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
        AudioManager.Instance.PlayAudio(saveSFX,1);
        UpdateSaveSlot();
    }

    public void LoadData()
    {
        saveData = SaveSystem.LoadSave("save" + slotIndex);
        SaveSystem.SetCurrentSaveData(saveData);
        if (SaveSystem.currentSaveData != null && DoesSceneExist(SaveSystem.currentSaveData.sceneName))
        {
            PlayerData.LoadFromSave();
            //FindObjectOfType<SceneLoading>().LoadScene(SaveSystem.currentSaveData.sceneName,0);
            //PlayerData.LoadFromSave();
            Time.timeScale = 1f;
            AudioManager.Instance.PlayAudio(saveSFX, 1);
            //FindObjectOfType<SceneLoading>().LoadScene(saveData.sceneName,0);
            FindObjectOfType<SceneLoading>().LoadScene(SaveSystem.currentSaveData.sceneName, 0);
        }
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
            SaveSystem.SetCurrentSaveData(saveData);
        }
    }

    public static bool DoesSceneExist(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var lastSlash = scenePath.LastIndexOf("/");
            var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

            if (string.Compare(name, sceneName, true) == 0)
                return true;
        }

        return false;
    }
}
