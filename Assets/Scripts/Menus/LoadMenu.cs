using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] private GameObject saveSlotPrefab;
    [SerializeField] private GameObject loadGridPanel;

    private List<GameObject> saveSlotList = new List<GameObject>();

    public void SetupSaveMenu()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject saveSlot = Instantiate(saveSlotPrefab);
            saveSlot.transform.SetParent(loadGridPanel.transform);

            SaveData saveData = SaveSystem.LoadSave("save" + i);
            saveSlot.GetComponent<SaveSlotButton>().SetupSaveSlot(saveData, i);
            saveSlotList.Add(saveSlot);
        }
    }

    public void CloseLoadMenu()
    {
        foreach (GameObject saveSlot in saveSlotList)
        {
            Destroy(saveSlot);
        }
        saveSlotList.Clear();
    }

}
