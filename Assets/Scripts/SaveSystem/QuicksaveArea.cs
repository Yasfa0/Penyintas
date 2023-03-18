using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuicksaveArea : MonoBehaviour
{
    private bool saved;
    [SerializeField] private GameObject quicksavePrefab;
    [SerializeField] private AudioClip saveSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !saved)
        {
            SaveData tempSave = new SaveData();
            tempSave.isQuicksave = true;
            tempSave.sceneName = SceneManager.GetActiveScene().name;
            //Dummy
            Vector3 pos = FindObjectOfType<PlayerCharacter>().transform.position;
            tempSave.posX = pos.x;
            tempSave.posY = pos.y;
            tempSave.health = FindObjectOfType<PlayerCharacter>().GetHealth();
            SaveSystem.SaveGame(tempSave, "save3");
            SaveSystem.SetCurrentSaveData(tempSave);

            saved = true;
            StartCoroutine(IndicateQuicksave());
        }
    }

    public IEnumerator IndicateQuicksave()
    {
        AudioManager.Instance.PlayAudio(saveSFX,1);
        GameObject quicksave = Instantiate(quicksavePrefab);
        yield return new WaitForSeconds(2f);
        Destroy(quicksave);
    }

}
