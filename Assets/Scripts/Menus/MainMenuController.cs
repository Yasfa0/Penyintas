using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string firstStage;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject blur;

    private GameObject lastSelected;

    private void Start()
    {
        //Cek apakah ada save data
        bool saveAvailable = false;
        for (int i = 0; i < 4; i++)
        {
            if (SaveSystem.CheckSaveSlot(i))
            {
                saveAvailable = true;
            }
        }
        //Kalau nggak ada, hide continue button
        if (!saveAvailable)
        {
            continueButton.SetActive(false);
            //Highligh play button
            Button[] menuButtons = FindObjectsOfType<Button>();
            foreach (Button button in menuButtons)
            {
                if (button.gameObject.name.Equals("PlayButton"))
                {
                    EventSystem.current.SetSelectedGameObject(button.gameObject);
                }
            }
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
        }

        //Set default keybind kalau belom ada keybind sama sekali
        if (KeybindSaveSystem.LoadKeybind() == null)
        {
            ControlKeybind defaultKeybind = new ControlKeybind();
            KeybindSaveSystem.SaveKeybind(defaultKeybind);
            KeybindSaveSystem.SetCurrentKeybind(KeybindSaveSystem.LoadKeybind());
            Debug.Log("Keybind set to default");
        }

        //AudioManager.Instance.PlayAudio(menuMusic, 0);
        AudioManager.Instance.PlayLoopingAudio(menuMusic,0);
        blur.SetActive(true);
        blur.GetComponent<Animator>().SetBool("blurOut",true);
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if(lastSelected != null)
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
            }
        }
        else
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void NewGame()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        PlayerData.SetSpawnPosition(new Vector2(0,0));
        PlayerData.SetHealth(1);

        //Create Temp Save
        /*SaveData tempSave = new SaveData();
        tempSave.isQuicksave = true;
        tempSave.sceneName = "Stage 1";
        Vector3 pos = new Vector3();
        tempSave.posX = pos.x;
        tempSave.posY = pos.y;
        tempSave.health = 1;

        if (SaveSystem.currentSaveData != null)
        {
            tempSave.eventKey = SaveSystem.currentSaveData.eventKey;
        }
        else
        {
            tempSave.eventKey = new bool[100];
        }


        SaveSystem.SaveGame(tempSave, "saveTemp");
        SaveSystem.SetCurrentSaveData(tempSave);
        Debug.Log("Temp Save Created");*/
        //Done Saving

        FindObjectOfType<SceneLoading>().LoadScene(firstStage,1);
    }

    public void OpenLoadMenu()
    {
        AudioManager.Instance.PlayAudio(buttonSound,1);
        loadPanel.SetActive(true);
        //blur.SetActive(true);
        blur.GetComponent<Animator>().SetBool("blurOut", false);
        menuPanel.SetActive(false);
        FindObjectOfType<LoadMenu>().SetupSaveMenu();
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
    }

    public void CloseLoadMenu()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        loadPanel.SetActive(false);
        loadPanel.GetComponent<LoadMenu>().CloseLoadMenu();
        //blur.SetActive(false);
        blur.GetComponent<Animator>().SetBool("blurOut", true);
        menuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
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
        //blur.SetActive(true);
        blur.GetComponent<Animator>().SetBool("blurOut", false);
        optionPanel.SetActive(true);

        FindObjectOfType<OptionMenu>().SetupOption();
        //FindObjectOfType<KeybindOption>().SetupControlOption();
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
    }

    public void CloseOption()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        menuPanel.SetActive(true);
        //blur.SetActive(false);
        blur.GetComponent<Animator>().SetBool("blurOut", true);
        optionPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
    }

    public void OpenCredit()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        menuPanel.SetActive(false);
        //blur.SetActive(true);
        blur.GetComponent<Animator>().SetBool("blurOut", false);
        creditPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
    }

    public void CloseCredit()
    {
        menuPanel.SetActive(true);
        //blur.SetActive(false);
        blur.GetComponent<Animator>().SetBool("blurOut", true);
        creditPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<Button>().gameObject);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayAudio(buttonSound, 1);
        Application.Quit();
    }
}