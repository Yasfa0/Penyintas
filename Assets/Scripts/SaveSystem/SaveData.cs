using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public bool isQuicksave;
    public string sceneName;
    public float posX, posY;

    public SaveData()
    {
        isQuicksave = false;
        sceneName = "";
        posX = 0;
        posY = 0;
    }
}
