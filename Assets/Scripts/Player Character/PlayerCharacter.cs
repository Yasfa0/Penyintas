using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    private int health = 1;

    [SerializeField] private Transform sightTarget;
    [SerializeField] private GameObject gameOverPrefab;
    private Animator animator;
    private RuntimeAnimatorController animatorController;

    private bool isGameOver;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        transform.position = PlayerData.GetSpawnPosition();
        animator = GetComponent<Animator>();
        animatorController = animator.runtimeAnimatorController;

        /*if (SaveSystem.currentSaveData != null)
        {
            SaveData save = SaveSystem.currentSaveData;
            Vector2 tempPos = new Vector3(save.posX, save.posY);
            spawnPosition = tempPos;
            health = SaveSystem.currentSaveData.health;
        }

        if (spawnPosition != Vector2.zero)
        {
            transform.position = spawnPosition;
        }*/

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !isGameOver)
        {
            isGameOver = true;
            //Jalanin animasi mati
            playerMovement.SetCanMove(false);
            playerMovement.StopMovement();
            //SceneManager.LoadScene("GameOver");
            Instantiate(gameOverPrefab);
        }
    }

    public int GetHealth()
    {
        return health;
    }
    
    public Transform GetSightTarget()
    {
        return sightTarget;
    }

    public void EraseAnimController()
    {
        animator.runtimeAnimatorController = null;
    }

    public void ReassignAnimController()
    {
        animator.runtimeAnimatorController = animatorController;
    }

}
