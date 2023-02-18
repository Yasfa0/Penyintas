using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionPanel;
    private bool isPaused = false;


    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        optionPanel.SetActive(false);
        isPaused = false;
    }

    public void OpenOption()
    {
        pausePanel.SetActive(false);
        optionPanel.SetActive(true);

        FindObjectOfType<OptionMenu>().SetupOption();
    }


    public void CloseOption()
    {
        pausePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void ToMainMenu()
    {
        FindObjectOfType<SceneLoading>().LoadScene("MainMenu");
    }
}
