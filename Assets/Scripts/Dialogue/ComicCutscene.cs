using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ComicCutscene : MonoBehaviour,IDialogueEvent
{
    [SerializeField] private int eventID;
    [SerializeField] private GameObject comicPrefab;
    PlayableDirector playableComic;

    public int GetEventId()
    {
        return eventID;
    }

    public void StartEvent()
    {
        playableComic = Instantiate(comicPrefab).GetComponent<PlayableDirector>();
        playableComic.Play();
    }

    private void Update()
    {
        if (playableComic && playableComic.state != PlayState.Playing)
        {
            Destroy(playableComic.gameObject);
            DialogueController.Instance.SetTalking(false);
            DialogueController.Instance.SetDoneWriting(true);
        }else if (playableComic)
        {
            DialogueController.Instance.SetTalking(true);
            DialogueController.Instance.SetDoneWriting(false);
        }
    }

}
