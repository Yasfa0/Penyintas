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
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject loadPanel;

    

    private void Start()
    {
        AudioManager.Instance.PlayAudio(menuMusic, 0);
    }

    public void NewGame()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        FindObjectOfType<SceneLoading>().LoadScene(firstStage);
    }

    public void OpenLoadMenu()
    {
        AudioManager.Instance.PlayAudio(buttonSound,1);
        loadPanel.SetActive(true);
        menuPanel.SetActive(false);
        FindObjectOfType<LoadMenu>().SetupSaveMenu();
    }

    public void CloseLoadMenu()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        loadPanel.SetActive(false);
        loadPanel.GetComponent<LoadMenu>().CloseLoadMenu();
        menuPanel.SetActive(true);
    }

    public void OpenMenu()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
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
        AudioManager.Instance.PlayAudio(buttonSound, 1);
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
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        Application.Quit();
    }
}