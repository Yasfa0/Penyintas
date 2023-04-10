using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class KeybindSaveSystem : MonoBehaviour
{
    public static ControlKeybind currentKeybind;

    public static void SaveKeybind(ControlKeybind keybind)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/keybind.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, keybind);
        stream.Close();
    }

    public static ControlKeybind LoadKeybind()
    {
        string path = Application.persistentDataPath + "/keybind.dat";
        ControlKeybind tempKeybind = new ControlKeybind();

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            tempKeybind = formatter.Deserialize(stream) as ControlKeybind;
            stream.Close();

            Debug.Log("Keybind Loaded");
            return tempKeybind;
        }
        else
        {
            Debug.Log("Cant find keybind");
            return null;
        }
    }

    public static void SetCurrentKeybind(ControlKeybind keybind)
    {
        currentKeybind = keybind;
    }
}
