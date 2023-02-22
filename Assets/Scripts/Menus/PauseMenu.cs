using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionPanel;
    private bool isPaused = false;

    [SerializeField] private AudioClip btnSound;


    public void PauseGame()
    {
        AudioManager.Instance.PlayAudio(btnSound,1);
        Debug.Log("Game Paused");
        isPaused = true;
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        AudioManager.Instance.PlayAudio(btnSound, 1);
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        isPaused = false;
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
        AudioManager.Instance.PlayAudio(btnSound, 1);
        FindObjectOfType<SceneLoading>().LoadScene("MainMenu");
    }

    public void PauseButtonVisibility(bool isVisible)
    {
        pauseButton.SetActive(isVisible);
    }
}
