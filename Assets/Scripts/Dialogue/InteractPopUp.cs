using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPopUp : MonoBehaviour
{
    [SerializeField] GameObject popUpPrefab;
    private bool canInteract = false;
    bool instantiated = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeybindSaveSystem.currentKeybind.interact) && canInteract && !instantiated)
        {
            if (FindObjectOfType<PlayerMovement>())
            {
                Debug.Log("Player Movement False");
                FindObjectOfType<PlayerMovement>().SetCanMove(false);
                FindObjectOfType<PlayerMovement>().StopMovement();
            }
            GameObject popUp =  Instantiate(popUpPrefab);
            popUp.GetComponent<PopUpPrasasti>().SetInteractPopUp(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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

    public void SetInstantiated(bool value)
    {
        instantiated = value;
    }

}
