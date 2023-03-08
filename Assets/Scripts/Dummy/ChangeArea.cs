using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArea : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Vector3 spawnPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<DummyMovement>().SetSpawnPosition(spawnPosition);
            FindObjectOfType<SceneLoading>().LoadScene(sceneName);
        }
    }
}
