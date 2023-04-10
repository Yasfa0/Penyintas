using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private List<PlayableDirector> timelines = new List<PlayableDirector>();
    [SerializeField] private GameObject playerCutsceneDummy;
    private GameObject playerCharacter;
    private bool cutsceneTriggered = false;

    private void Awake()
    {
        playerCharacter = FindObjectOfType<PlayerCharacter>().gameObject;
    }

    public void StopPlayerInput()
    {
        //FindObjectOfType<PlayerMovement>().StopMovement();
        //FindObjectOfType<PlayerMovement>().SetCanMove(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !cutsceneTriggered)
        {
            Debug.Log("Speak");
            cutsceneTriggered = true;
            PlayTimeline(0);
        }
    }

    public void PlayTimeline(int directorIndex)
    {
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
        StopAllTimeline();
        timelines[directorIndex].Play();
    }

    public void StopAllTimeline()
    {
        foreach (PlayableDirector time in timelines)
        {
            time.Stop();
        }
    }

    public void SetupCutscene()
    {
        //FindObjectOfType<PlayerCharacter>().EraseAnimController();
        //FindObjectOfType<PlayerCharacter>().gameObject.SetActive(false);
        playerCharacter.GetComponent<PlayerMovement>().StopMovement();
        playerCharacter.GetComponent<PlayerMovement>().SetCanMove(false);
        //playerCharacter.GetComponent<Animator>().SetFloat("Speed",0);
        //playerCharacter.GetComponent<Animator>().SetBool("isCrouch",false);
        playerCharacter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //playerCutsceneDummy.transform.position = playerCharacter.transform.position;
        //playerCharacter.SetActive(false);
    }

    public void EndCutscene()
    {
        //FindObjectOfType<PlayerCharacter>().ReassignAnimController();
        //FindObjectOfType<PlayerCharacter>().gameObject.SetActive(true);
        playerCharacter.SetActive(true);
        //playerCharacter.transform.position = playerCutsceneDummy.transform.position;
        playerCharacter.GetComponent<PlayerMovement>().SetCanMove(true);
        playerCharacter.GetComponent<PlayerMovement>().ResetSpeed();
        //playerCutsceneDummy.SetActive(false);
        FindObjectOfType<CameraFollowTarget>().SetCanFollow(true);
        FindObjectOfType<Camera>().orthographicSize = 5;
    }

    public void EndCutsceneEvent()
    {
        StopAllTimeline();
        EndCutscene();
    }
}
