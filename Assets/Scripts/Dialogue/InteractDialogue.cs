using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDialogue : DialogueSpeaker
{
    private bool canInteract = false;
    [SerializeField] private bool faceToPlayer = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeybindSaveSystem.currentKeybind.interact) && canInteract)
        {
            if (!DialogueController.Instance.GetTalking())
            {
                FacePlayer();
                DialogueController.Instance.SetCurrentSpeaker(this);
                DialogueController.Instance.SetupDialogue(dialogueList);
                Debug.Log("Speak");
            }
        }
    }

    public void FacePlayer()
    {
        if (faceToPlayer)
        {
            Transform playerTrans = FindObjectOfType<PlayerCharacter>().GetComponent<Transform>();
            if (playerTrans.position.x > gameObject.transform.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (playerTrans.position.x < gameObject.transform.position.x)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canInteract = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInteract = false;
        }
    }
}
