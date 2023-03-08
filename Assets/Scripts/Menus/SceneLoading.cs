using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject blackScreen;

    private void Awake()
    {
        StartCoroutine(FadeBlackScreen("FadeIn", false));   
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingScreen(sceneName));
    }

    IEnumerator LoadingScreen(string sceneName)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);
        if (!loadAsync.isDone)
        {
            float progress = Mathf.Clamp01(loadAsync.progress / 0.9f);
            loadingScreen.GetComponentInChildren<Slider>().value = progress;
            yield return null;
        }
    }

    public IEnumerator FadeBlackScreen(string anim,bool visible)
    {
        if (blackScreen)
        {
            blackScreen.SetActive(true);
            blackScreen.GetComponent<Animator>().SetBool(anim, true);
            yield return new WaitForSeconds(1.1f);
            blackScreen.SetActive(visible);
        }
    }
}
