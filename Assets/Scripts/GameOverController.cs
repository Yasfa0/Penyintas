using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private float inputWait;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime >= inputWait)
        {
            if (Input.anyKeyDown)
            {
                if (SaveSystem.currentSaveData != null && DoesSceneExist(SaveSystem.currentSaveData.sceneName))
                {
                    PlayerData.LoadFromSave();
                    FindObjectOfType<SceneLoading>().LoadScene(SaveSystem.currentSaveData.sceneName);
                }
                else
                {
                    FindObjectOfType<SceneLoading>().LoadScene("MainMenu");
                }
            }
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
