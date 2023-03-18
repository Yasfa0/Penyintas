using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private List<GameObject> loadingScreen;
    [SerializeField] private GameObject blackScreen;

    private void Awake()
    {
        StartCoroutine(FadeBlackScreen("FadeIn", false));   
    }

    public void LoadScene(string sceneName, int typeIndex)
    {
        StartCoroutine(LoadingScreen(sceneName,typeIndex));
    }

    IEnumerator LoadingScreen(string sceneName, int typeIndex)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen[typeIndex].SetActive(true);
        if (!loadAsync.isDone)
        {
            //float progress = Mathf.Clamp01(loadAsync.progress / 0.9f);
            float progress = Mathf.Clamp01(loadAsync.progress);
            Debug.Log("Progress " + progress);
            loadingScreen[typeIndex].GetComponentInChildren<Slider>().value = progress;
            yield return null;
        }
        if (FindObjectOfType<PlayerMovement>())
        {
            FindObjectOfType<PlayerMovement>().SetCanMove(true);
        }
        if (FindObjectOfType<PauseMenu>())
        {
            FindObjectOfType<PauseMenu>().SetIsPause(false);
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
