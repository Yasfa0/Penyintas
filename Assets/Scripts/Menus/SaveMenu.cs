using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] private GameObject saveSlotPrefab;
    [SerializeField] private GameObject savePanel;
    [SerializeField] private GameObject saveGridPanel;

    private List<GameObject> saveSlotList = new List<GameObject>();

    public void SetupSaveMenu()
    {
        savePanel.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            GameObject saveSlot = Instantiate(saveSlotPrefab);
            saveSlot.transform.SetParent(saveGridPanel.transform);

            SaveData saveData = SaveSystem.LoadSave("save"+i);
            saveSlot.GetComponent<SaveSlotButton>().SetupSaveSlot(saveData,i);
            saveSlotList.Add(saveSlot);
        }
    }

    public void CloseSaveMenu()
    {
        foreach (GameObject saveSlot in saveSlotList)
        {
            Destroy(saveSlot);
        }
        saveSlotList.Clear();
        savePanel.SetActive(false);
    }

}
