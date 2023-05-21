using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public bool isQuicksave;
    public string sceneName;
    public float posX, posY;
    public int health;

    public bool[] eventKey = new bool[100];

    /*Event Key List
     * 1 = Starting Comic
     * 2 = Corpse Hiding Spot Dialogue
     * 61 = Bantu Kardi
     * 99 = Ambil uang
     * 98 = Ambil kontrak
     * 97 = Ambil foto
     * End List 
    */

    public SaveData()
    {
        isQuicksave = false;
        sceneName = "";
        posX = 0;
        posY = 0;
        health = 1;
    }
}
