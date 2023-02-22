using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class GameSetting
{
    public static void SaveSetting(SettingData settingData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        //SettingData settingData = new SettingData();
        formatter.Serialize(stream,settingData);
        stream.Close();
    }

    public static SettingData LoadSetting()
    {
        string path = Application.persistentDataPath + "/settings.dat";
        SettingData settingData = new SettingData();

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            settingData = formatter.Deserialize(stream) as SettingData;
            stream.Close();

            Debug.Log("Settings Loaded");
            return settingData;
        }
        else
        {
            Debug.Log("Cant find setting file");
            return settingData;
        }
    }
}
