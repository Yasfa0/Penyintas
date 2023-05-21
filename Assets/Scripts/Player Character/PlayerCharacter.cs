using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerCharacter : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    [SerializeField] private int health = 1;
    private bool immune = false;

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

    [SerializeField] float kecepatanInjured = 1f;
    [SerializeField] float kecepatanHealthy = 2f;
    [SerializeField] private List<SpriteRenderer> injuredHands = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> healthyHands = new List<SpriteRenderer>();

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (!Vector2.Equals(PlayerData.GetSpawnPosition(),Vector2.zero))
        {
            transform.position = PlayerData.GetSpawnPosition();
        }

        //health = PlayerData.GetHealth();
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

        //Kalau nggak ada custom keybind, balik ke setting default 
        if (KeybindSaveSystem.LoadKeybind() == null)
        {
            ControlKeybind defaultKeybind = new ControlKeybind();
            KeybindSaveSystem.SaveKeybind(defaultKeybind);
        }

        KeybindSaveSystem.SetCurrentKeybind(KeybindSaveSystem.LoadKeybind());
    }


    private void Start()
    {
        UpdateHealthAnim();
    }

    private void Update()
    {
        //Untuk testing doang
        /*if (Input.GetKey(KeyCode.Alpha1))
        {
            SetHealth(1);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SetHealth(2);
        }*/

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
        if (!immune)
        {
            health -= damage;
            if (health <= 0 && !isGameOver)
            {
                isGameOver = true;
                //Jalanin animasi mati
                playerMovement.SetCanMove(false);
                playerMovement.StopMovement();
                playerMovement.SetPlayerDead(true);
                animator.SetBool("isDead",true);
                //SceneManager.LoadScene("GameOver");
                Instantiate(gameOverPrefab);
            }

            UpdateHealthAnim();
        }
        
    }

    public void SetHealth(int health)
    {
        this.health = health;
        UpdateHealthAnim();
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

    public void UpdateHealthAnim()
    {
        bool injuHand = false;
        bool healthHand = false;
        if(health == 1)
        {
            //Injured
            animator.SetTrigger("isInjured");
            animator.ResetTrigger("Healthy");
            injuHand = true;
            GetComponent<PlayerMovement>().SetKecepatanMaks(kecepatanInjured);
            
        }
        else if(health >= 2)
        {
            //Healthy
            animator.SetTrigger("Healthy");
            animator.ResetTrigger("isInjured");
            healthHand = true;
            GetComponent<PlayerMovement>().SetKecepatanMaks(kecepatanHealthy);
        }


        foreach (SpriteRenderer injuredHand in injuredHands)
        {
            injuredHand.enabled = injuHand;
        }
        foreach (SpriteRenderer healthyHand in healthyHands)
        {
            healthyHand.enabled = healthHand;
        }

        GetComponent<PlayerMovement>().SetCanJump(healthHand);
        GetComponent<PlayerMovement>().SetCanRun(healthHand);
    }

    public void SetImmune(bool immune)
    {
        this.immune = immune;
    }

    public bool GetImmune()
    {
        return immune;
    }


}
