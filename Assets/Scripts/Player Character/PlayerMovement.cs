using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static bool canMove = true;

    //[SerializeField] private float speed = 3f;
    [SerializeField] private float jumpStrength = 200f;

    [Header("Character Attribute")]
    Rigidbody2D rb;
    Animator anim;
    Transform tanganKiriIK;

    [Space(10)]

    [Header("Character Speed with Acceleration System")]
    float kecepatan = 0f;
    float kecepatanMaks = 1f;
    float initKecepatan;
    float Akselerasi = 2f;
    float Deselerasi = 10f;
    float initAksel,initDecel;
    float runAksel, runDecel;
    [Space(10)]

    [Header("Sprite")]
    public SpriteRenderer tanganKanan;
    public SpriteRenderer tanganKiri;
    public SpriteRenderer tanganKananTerluka;
    public SpriteRenderer tanganKiriTerluka;
    [Space(10)]

    [Header("Grabable GameObject Setting")]
    DetectGrab detect;
    [Space(10)]

    [Header("Jumping")]
    int jumpLimit = 1;
    int jumpCounter;

    bool bisaJongkok;
    bool playerDead;

    public bool isGrabbing;
    bool canJump, canRun = false;
    [HideInInspector] public bool attachRope;

    //float move;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        detect = GetComponentInChildren<DetectGrab>();
        tanganKiriIK = transform.Find("Tangan Kiri");
        initAksel = Akselerasi;
        initDecel = Deselerasi;
        runAksel = initAksel * 2;
        runDecel = initDecel * 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        playerDead = false;
        isGrabbing = false;
        attachRope = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (canMove && !PauseMenu.Instance.GetIsPaused())
        {
            if (!Mathf.Approximately(0, move))
            {
                transform.rotation = -move > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !anim.GetBool("isCrouch"))
            {
                //speed = baseSpeed * runMultiplier;
                speed = 10f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || anim.GetBool("isCrouch"))
            {
                //speed = baseSpeed;
                speed = 3f;
            }

            move = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(move * speed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("isJump");
                rb.AddForce(new Vector2(rb.velocity.x, jumpStrength));
            }

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                anim.SetBool("isCrouch", true);
            }
            else
            {
                anim.SetBool("isCrouch", false);
            }

            anim.SetFloat("Speed", Mathf.Abs(move * speed));
        }*/

        anim.SetBool("isDead", playerDead); // animasi player mati

        if (canMove && !PauseMenu.Instance.GetIsPaused())
        {
            if (!playerDead)
            {
                if (kecepatanMaks != 1 && anim.GetBool("isInjured"))
                    anim.SetFloat("kecepatan", kecepatanMaks - 0.3f); // kecepatan animasi jika kecepatan player dinaikkan
                else
                    anim.SetFloat("kecepatan", 1);

                if (!isGrabbing)
                {
                    //Flip Character
                    if (!Mathf.Approximately(0, kecepatan))
                    {
                        transform.rotation = -kecepatan > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
                        tanganKananTerluka.sortingOrder = 46;
                        tanganKiriTerluka.sortingOrder = 36;
                        if (transform.rotation == Quaternion.Euler(0, 180, 0))
                        {
                            tanganKananTerluka.sortingOrder = 36;
                            tanganKiriTerluka.sortingOrder = 46;
                        }
                    }
                }
                PlayerMove();
                PlayerJump();
                PlayerRun();
                GrabObject();
            }
        }

    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeybindSaveSystem.currentKeybind.jump) && canJump && jumpCounter < jumpLimit)
        {
            jumpCounter++;
            anim.SetBool("isJump",true);
        }
    }

    public void JumpForce()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpStrength));
    }

    public void PlayerRun()
    {
        if (canRun)
        {
            if (Input.GetKey(KeybindSaveSystem.currentKeybind.run) && !anim.GetBool("canCrouch"))
            {
                //kecepatan = 3.7f;
                kecepatanMaks = 6f;
                Akselerasi = runAksel;
                Deselerasi = runDecel;
            }
            else
            {
                kecepatanMaks = initKecepatan;
                Akselerasi = initAksel;
                Deselerasi = initDecel;
            }

            /*if (Input.GetKeyUp(KeybindSaveSystem.currentKeybind.run) || anim.GetBool("canCrouch"))
            {
                //kecepatan = initKecepatan;
                kecepatanMaks = initKecepatan;
                Akselerasi = initAksel;
                Deselerasi = initDecel;
            }*/
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isJump", false);
            jumpCounter = 0;
        }
    }

    void PlayerMove()
    {
        //Movement Animation
        anim.SetFloat("speed", Mathf.Abs(kecepatan));
        anim.SetBool("canCrouch", bisaJongkok);

        //Gerak Kiri Kanan
        if (Input.GetKey(KeybindSaveSystem.currentKeybind.moveLeft) && (kecepatan > -kecepatanMaks))
        {
            kecepatan = kecepatan - Akselerasi * Time.deltaTime;
        }
        else if (Input.GetKey(KeybindSaveSystem.currentKeybind.moveRight) && (kecepatan < kecepatanMaks))
        {
            kecepatan = kecepatan + Akselerasi * Time.deltaTime;
        }
        else
        {
            if (kecepatan > Deselerasi * Time.deltaTime)
            {
                kecepatan = kecepatan - Deselerasi * Time.deltaTime;
            }
            else if (kecepatan < -Deselerasi * Time.deltaTime)
            {
                kecepatan = kecepatan + Deselerasi * Time.deltaTime;
            }
            else
            {
                kecepatan = 0;
            }
        }
        transform.position = new Vector2(transform.position.x + kecepatan * Time.deltaTime, transform.position.y);

        //Jongkok
        if (Input.GetKey(KeybindSaveSystem.currentKeybind.crouch))
        {
            bisaJongkok = true;
        }
        else
        {
            //bisaJongkok = false;
        }

        if (Input.GetKeyUp(KeybindSaveSystem.currentKeybind.crouch))
        {
            bisaJongkok = false;
        }

    }

    void GrabObject()
    {
        if (detect.grabableObject != null && tanganKiriIK != null)
        {
            if (Input.GetKey(KeybindSaveSystem.currentKeybind.grab))
            {
                tanganKiriTerluka.enabled = false;
                tanganKiri.enabled = true;
                isGrabbing = true;

                tanganKiriIK.parent = detect.grabableObject.transform.GetChild(0);
                detect.grabableObject.parent = this.transform;
            }

            if (Input.GetKeyUp(KeybindSaveSystem.currentKeybind.grab))
            {
                tanganKiriTerluka.enabled = true;
                tanganKiri.enabled = false;
                isGrabbing = false;

                tanganKiriIK.parent = this.transform;
                detect.grabableObject.parent = null;
                //Destroy(GetComponent<HingeJoint2D>());
            }
        }

        if (detect.grabableObject == null)
        {
            tanganKiriTerluka.enabled = true;
            if (anim.GetBool("isInjured"))
            {
                tanganKiri.enabled = false;
            }
            if(attachRope == false)
            {
                tanganKiriIK.parent = this.transform;
                isGrabbing = false;
            }
        }
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        kecepatan = 0;
        //anim.SetFloat("Speed", Mathf.Abs(move * speed));
        anim.SetFloat("speed", Mathf.Abs(kecepatan));
        anim.SetBool("canCrouch", bisaJongkok);
        Debug.Log("Movement Stopped");
    }

    public void SetPlayerDead(bool playerDead)
    {
        this.playerDead = playerDead;
    }

    public void ResetSpeed()
    {
        //kecepatan = 1;
        //anim.SetFloat("Speed", Mathf.Abs(move * speed));
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public bool GetCanMove()
    {
        return canMove;
    }


    public bool GetCanJump()
    {
        return canJump;
    }

    public bool GetCanRun()
    {
        return canRun;
    }

    public void SetCanJump(bool canJump)
    {
        this.canJump = canJump;
    }


    public void SetCanRun(bool canRun)
    {
        this.canRun = canRun;
    }

    public void SetKecepatanMaks(float speed)
    {
        kecepatanMaks = speed;
        initKecepatan = speed;
    }

}