using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueController : MonoBehaviour
{
    //Untuk Singleton
    public static DialogueController Instance { get; private set; }

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Button skipButton;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text nameText;
    [SerializeField] private Transform choicePanel;
    [SerializeField] private GameObject choiceBoxPrefab;
    //[SerializeField] private GameObject skipButton;

    //[SerializeField] public List<DialogueScriptable> dialogueList = new List<DialogueScriptable>();
    List<DialogueScriptable> dialogueList = new List<DialogueScriptable>();
    private int dialogueRoute = 0;
    private int dialogueIndex = 0;

    private bool talking = false;
    private bool doneWriting = false;
    private bool selectingChoice = false;
    private DialogueSpeaker currentSpeaker;

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

        if (KeybindSaveSystem.LoadKeybind() == null)
        {
            ControlKeybind defaultKeybind = new ControlKeybind();
            KeybindSaveSystem.SaveKeybind(defaultKeybind);
        }

        KeybindSaveSystem.SetCurrentKeybind(KeybindSaveSystem.LoadKeybind());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeybindSaveSystem.currentKeybind.interact))
        {

            if (talking && doneWriting && !selectingChoice)
            {
                NextDialogue();
            }

        }
    }

    public void SetupDialogue(List<DialogueScriptable> dialogues)
    {
        dialogueBox.SetActive(true);
        talking = true;

        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(delegate { SkipDialogue(); });

        if (FindObjectOfType<PlayerCharacter>())
        {
            Debug.Log("Player Movement False");
            FindObjectOfType<PlayerMovement>().SetCanMove(false);
            FindObjectOfType<PlayerMovement>().StopMovement();
        }
        if (FindObjectOfType<PauseMenu>())
        {
            //FindObjectOfType<PauseMenu>().PauseButtonVisibility(false);
            FindObjectOfType<PauseMenu>().SetCanPause(false);
        }

        dialogueList.Clear();
        foreach (var dialogue in dialogues)
        {
            dialogueList.Add(dialogue);
        }

        dialogueRoute = 0;
        dialogueIndex = 0;

        Debug.Log("Setting up dialogue");
        ShowDialogue(dialogueList[dialogueRoute].dialogues[dialogueIndex]);
    }

    public void OpenDialogueRoute(List<DialogueScriptable> dialogues,int inputRoute)
    {
        dialogueBox.SetActive(true);
        talking = true;

        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(delegate { SkipDialogue(); });

        if (FindObjectOfType<PlayerCharacter>())
        {
            Debug.Log("Player Movement False");
            FindObjectOfType<PlayerMovement>().SetCanMove(false);
            FindObjectOfType<PlayerMovement>().StopMovement();
        }
        if (FindObjectOfType<PauseMenu>())
        {
            //FindObjectOfType<PauseMenu>().PauseButtonVisibility(false);
            FindObjectOfType<PauseMenu>().SetCanPause(false);
        }

        dialogueList.Clear();
        foreach (var dialogue in dialogues)
        {
            dialogueList.Add(dialogue);
        }

        dialogueRoute = inputRoute;
        dialogueIndex = 0;

        Debug.Log("Setting up dialogue");
        ShowDialogue(dialogueList[dialogueRoute].dialogues[dialogueIndex]);
    }

    public void ShowDialogue(Dialogue currentDialogue)
    {
        doneWriting = false;
        dialogueText.color = currentDialogue.textColor;
        dialogueText.text = " ";


        nameText.text = currentDialogue.name;
        Debug.Log("Showing Dialogue");
        StartCoroutine(TypeDialogue(currentDialogue.dialogue));
    }

    IEnumerator TypeDialogue(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        doneWriting = true;
        Debug.Log("Typing Dialogue");
        yield return null;
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        Debug.Log("Next dialogue");
        /*Debug.Log("Dialogue Length: " + dialogueList[dialogueRoute].dialogues.Length);
        Debug.Log("Dialogue Index: " + dialogueIndex);*/

        if (dialogueIndex < dialogueList[dialogueRoute].dialogues.Length)
        {
            ShowDialogue(dialogueList[dialogueRoute].dialogues[dialogueIndex]);
        }
        else
        {
            if (dialogueList[dialogueRoute].choices.Length > 0)
            {
                ShowChoices();
            }
            else
            {
                Debug.Log("No more dialogue to write");
                StartCoroutine(EndDialogue());
            }
        }
    }

    public void ShowChoices()
    {
        Debug.Log("Showing Choices");
        foreach (Choice choice in dialogueList[dialogueRoute].choices)
        {
            GameObject choiceBox = Instantiate(choiceBoxPrefab);
            choiceBox.transform.SetParent(choicePanel);
            choiceBox.GetComponentInChildren<Text>().text = choice.text;
            choiceBox.GetComponent<ChoiceButton>().SetupChoice(choice.choiceIndex);
            EventSystem.current.SetSelectedGameObject(choiceBox);
        }
        selectingChoice = true;
    }

    public void EraseChoice()
    {
        foreach (var choiceButton in choicePanel.GetComponentsInChildren<ChoiceButton>())
        {
            Destroy(choiceButton.gameObject);
        }
    }

    public void SkipDialogue()
    {
        Debug.Log("Skip Dialogue");
        Debug.Log("Current Speaker : " + currentSpeaker);
        StartCoroutine(EndDialogue());
    }

    public IEnumerator EndDialogue()
    {
        currentSpeaker.ExecuteEvent(dialogueList[dialogueRoute].eventIndex);
        dialogueRoute = 0;
        dialogueIndex = 0;
        dialogueBox.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        talking = false;
        Debug.Log("Ending dialogue");
        //Dummy doang. Nanti hapus setelah ada kodingan player
        if (FindObjectOfType<PlayerMovement>())
        {
            Debug.Log("Player Movement True");
            FindObjectOfType<PlayerMovement>().SetCanMove(true);
            FindObjectOfType<PlayerMovement>().ResetSpeed();
        }

        if (FindObjectOfType<PauseMenu>())
        {
            //FindObjectOfType<PauseMenu>().PauseButtonVisibility(true);
            FindObjectOfType<PauseMenu>().SetCanPause(true);
        }
    }

    public void ApplyChoice(int route)
    {
        dialogueRoute = route;
        dialogueIndex = -1;
        EraseChoice();
        selectingChoice = false;
        NextDialogue();
    }

    //GETTER & SETTER

    public bool GetTalking()
    {
        return talking;
    }

    public void SetTalking(bool talking)
    {
        this.talking = talking;
    }

    public void SetDoneWriting(bool doneWriting)
    {
        this.doneWriting = doneWriting;
    }

    public void SetCurrentSpeaker(DialogueSpeaker speaker)
    {
       currentSpeaker = speaker;
    }

    public GameObject GetDialogueBox()
    {
        return dialogueBox;
    }

    public Text GetDialogueText()
    {
        return dialogueText;
    }

    public Button GetSkipButton()
    {
        return skipButton;
    }

    public Text GetNameText()
    {
        return nameText;
    }

    public Transform GetChoicePanel()
    {
        return choicePanel;
    }

    public GameObject GetChoicePrefab()
    {
        return choiceBoxPrefab;
    }
}
