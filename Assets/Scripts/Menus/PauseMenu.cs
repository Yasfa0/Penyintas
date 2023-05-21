using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    //[SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject loadPanel;

    [SerializeField] private AudioClip btnSound;
    private static bool isPaused;
    private bool canPause = true;

    [SerializeField] private Button firstSelected;
    [SerializeField] private GameObject optionFocus;
    [SerializeField] private GameObject loadFocus;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        //AudioManager.Instance.PlayAudio(btnSound,1);
        Debug.Log("Game Paused");
        canPause = false;
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
        isPaused = true;
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    public void ResumeGame()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        canPause = true;
    }

    public void OpenLoadMenu()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        loadPanel.SetActive(true);
        pausePanel.SetActive(false);
        FindObjectOfType<LoadMenu>().SetupSaveMenu();
        EventSystem.current.SetSelectedGameObject(loadFocus);
    }

    public void CloseLoadMenu()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        loadPanel.SetActive(false);
        loadPanel.GetComponent<LoadMenu>().CloseLoadMenu();
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    public void OpenOption()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        pausePanel.SetActive(false);
        optionPanel.SetActive(true);

        optionPanel.GetComponent<OptionMenu>().SetupOption();
        //FindObjectOfType<OptionMenu>().SetupOption();
        EventSystem.current.SetSelectedGameObject(optionFocus);
    }


    public void CloseOption()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelected.gameObject);
    }

    public void ToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioManager.Instance.PlayAudio(btnSound, 1);
        FindObjectOfType<SceneLoading>().LoadScene("MainMenu",0);
    }

    public void PauseButtonVisibility(bool isVisible)
    {
        //pauseButton.SetActive(isVisible);
    }

    public void SetCanPause(bool value)
    {
        canPause = value;
    }

    public void SetIsPause(bool value)
    {
        isPaused = value;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

}
