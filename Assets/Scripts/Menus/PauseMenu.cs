using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionPanel;

    [SerializeField] private AudioClip btnSound;
    private static bool isPaused;

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

    public void PauseGame()
    {
        AudioManager.Instance.PlayAudio(btnSound,1);
        Debug.Log("Game Paused");
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void OpenOption()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        pausePanel.SetActive(false);
        optionPanel.SetActive(true);

        FindObjectOfType<OptionMenu>().SetupOption();
    }


    public void CloseOption()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void ToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        AudioManager.Instance.PlayAudio(btnSound, 1);
        FindObjectOfType<SceneLoading>().LoadScene("MainMenu");
    }

    public void PauseButtonVisibility(bool isVisible)
    {
        pauseButton.SetActive(isVisible);
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

}
