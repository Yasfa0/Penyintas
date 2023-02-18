using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string firstStage;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionPanel;

    

    private void Start()
    {
        AudioManager.Instance.PlayAudio(menuMusic, 0);
    }

    public void NewGame()
    {
        FindObjectOfType<SceneLoading>().LoadScene(firstStage);
    }

    public void OpenMenu()
    {
        StartCoroutine(DelayOpenMenu());
    }

    public IEnumerator DelayOpenMenu()
    {
        startPanel.GetComponent<Animator>().SetBool("dissapear", true);
        yield return new WaitForSeconds(0.5f);
        startPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OpenOption()
    {
        StartCoroutine(DelayOpenOption());
    }

    public IEnumerator DelayOpenOption()
    {
        menuPanel.GetComponent<Animator>().SetBool("SlideOut",true);
        yield return new WaitForSeconds(0.5f);
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);

        FindObjectOfType<OptionMenu>().SetupOption();
    }

    public void CloseOption()
    {
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}