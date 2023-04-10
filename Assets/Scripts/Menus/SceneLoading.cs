using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private List<GameObject> loadingScreen;
    [SerializeField] private GameObject blackScreen;
    private float delay = 0.5f;
    private bool delayDone = false;
    bool canLoad = true;

    private void Awake()
    {
        FadeToBlack();   
    }

    public void LoadScene(string sceneName, int typeIndex)
    {
        if (FindObjectOfType<PlayerCharacter>())
        {
            FindObjectOfType<PlayerCharacter>().SetImmune(true);
        }
        StartCoroutine(LoadingScreen(sceneName,typeIndex));
    }

    IEnumerator LoadingScreen(string sceneName, int typeIndex)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen[typeIndex].SetActive(true);
        while (!loadAsync.isDone && canLoad)
        {
            //float progress = Mathf.Clamp01(loadAsync.progress / 0.9f);
            float progress = Mathf.Clamp01(loadAsync.progress /.9f);
            Debug.Log("Progress " + progress);
            loadingScreen[typeIndex].GetComponentInChildren<Slider>().value = progress;
            if (!delayDone && progress >= 0.7f)
            {
                StartCoroutine(Delay());
            }
            yield return null;
        }
    }

    public IEnumerator Delay()
    {
        canLoad = false;
        yield return new WaitForSeconds(delay);
        delayDone = true;
        canLoad = true;
    }

    public void FadeToBlack()
    {
        StartCoroutine(FadeBlackScreen("FadeIn", false));
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
