using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPrasasti : MonoBehaviour
{
    [Header("Game Object Reference")]
    [SerializeField] Image titleImg;
    [SerializeField] Text contentText;
    [Space(10)]

    [Header("Pop Up Content")]
    [SerializeField] Sprite titleSprite;
    [TextArea(3,10)]
    [SerializeField] string content;

    bool doneWriting = false;
    InteractPopUp interactPopUp;

    private void Awake()
    {
        titleImg.sprite = titleSprite;
        //contentText.text = content;
    }

    private void Start()
    {
        StartCoroutine(TypeDialogue(content));
    }

    private void Update()
    {
        if (doneWriting && Input.GetKey(KeybindSaveSystem.currentKeybind.interact))
        {
            interactPopUp.SetInstantiated(false);
            if (FindObjectOfType<PlayerMovement>())
            {
                Debug.Log("Player Movement True");
                FindObjectOfType<PlayerMovement>().SetCanMove(true);
                FindObjectOfType<PlayerMovement>().ResetSpeed();
            }
            Destroy(gameObject);
        }
    }


    IEnumerator TypeDialogue(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            contentText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        doneWriting = true;
        Debug.Log("Typing Dialogue");
        yield return null;
    }

    public void SetInteractPopUp(InteractPopUp interactPopUp)
    {
        this.interactPopUp = interactPopUp;
        interactPopUp.SetInstantiated(true);
    }

}
