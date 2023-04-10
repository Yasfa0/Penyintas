using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    //private static bool loadingSave = false;
    private static Vector2 spawnPosition;
    private static int health;

    public static void LoadFromSave()
    {
        //loadingSave = true;
        Vector2 pos = new Vector2(SaveSystem.currentSaveData.posX,SaveSystem.currentSaveData.posY);
        spawnPosition = pos;
        health = SaveSystem.currentSaveData.health;
    }

    public static void SetSpawnPosition(Vector2 inputPosition)
    {
        //loadingSave = false;
        spawnPosition = inputPosition;
    }

    public static Vector2 GetSpawnPosition()
    {
        return spawnPosition;
    }

    public static void SetHealth(int val)
    {
        health = val;
    }

    public static int GetHealth()
    {
        return health;
    }
}
