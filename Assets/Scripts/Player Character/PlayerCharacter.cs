using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerCharacter : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    private int health = 1;

    [SerializeField] private Transform sightTarget;
    [SerializeField] private GameObject gameOverPrefab;
    [SerializeField] private Volume volume;
    private Vignette vignette;
    private float vignetteIntensity;
    private float vignetteLimit = 0.4f;
    
    private Animator animator;
    private RuntimeAnimatorController animatorController;

    private bool isGameOver;

    private bool isHidden;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (!Vector2.Equals(PlayerData.GetSpawnPosition(),Vector2.zero))
        {
            transform.position = PlayerData.GetSpawnPosition();
        }
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

    private void Update()
    {
        //Vignette Updater
        if (animator.GetBool("canCrouch") && isHidden)
        {
            if(vignetteIntensity < vignetteLimit)
            {
                vignetteIntensity += 1 * Time.deltaTime;
            }

            volume.profile.TryGet(out vignette);
            {
                vignette.intensity.value = vignetteIntensity;
                vignette.active = true;
            }
        }
        else
        {
            if (vignetteIntensity > 0)
            {
                vignetteIntensity -= 1 * Time.deltaTime;
            }

            volume.profile.TryGet(out vignette);
            {
                vignette.intensity.value = vignetteIntensity;
                //vignette.active = false;
            }
        }
    }

    public void SetHidden(bool isHidden)
    {
        this.isHidden = isHidden;
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
            playerMovement.SetPlayerDead(true);
            //animator.SetBool("isDead",true);
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
