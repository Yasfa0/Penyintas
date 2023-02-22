using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static SaveData currentSaveData;

    public static void SaveGame(SaveData saveData,string saveName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+saveName+".dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData LoadSave(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName + ".dat";
        SaveData saveData = new SaveData();

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Save Data Loaded");
            return saveData;
        }
        else
        {
            Debug.Log("Cant find save file");
            return saveData;
        }
    }

    public static void SetCurrentSaveData(SaveData save)
    {
        currentSaveData = save;
    }

}
