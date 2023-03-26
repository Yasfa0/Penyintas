using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static bool canMove = true;

    //[SerializeField] private float speed = 3f;
    //[SerializeField] private float jumpStrength = 200f;

    [Header("Character Attribute")]
    Rigidbody2D rb;
    Animator anim;
    Transform tanganKiriIK;

    [Space(10)]

    [Header("Character Speed with Acceleration System")]
    float kecepatan = 0f;
    public float kecepatanMaks = 1f;
    float Akselerasi = 2f;
    float Deselerasi = 10f;
    [Space(10)]

    [Header("Sprite")]
    public SpriteRenderer tanganKanan;
    public GameObject tanganKiri;
    public SpriteRenderer tanganKananTerluka;
    public SpriteRenderer tanganKiriTerluka;
    [Space(10)]

    [Header("Grabable GameObject Setting")]
    DetectGrab detect;
    [Space(10)]

    bool bisaJongkok;
    bool playerDead;
    bool isGrabbing;

    //float move;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        detect = GetComponentInChildren<DetectGrab>();
        tanganKiriIK = transform.Find("Tangan Kiri");
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        playerDead = false;
        isGrabbing = false;
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

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("canMove" + canMove);
            Debug.Log("kecepatan" + kecepatan);
        }

        anim.SetBool("isDead", playerDead); // animasi player mati

        if (canMove && !PauseMenu.Instance.GetIsPaused())
        {
            if (!playerDead)
            {
                if (kecepatanMaks != 1)
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
                GrabObject();
            }
        }

    }

    void PlayerMove()
    {
        //Movement Animation
        anim.SetFloat("speed", Mathf.Abs(kecepatan));
        anim.SetBool("canCrouch", bisaJongkok);

        //Gerak Kiri Kanan
        if (Input.GetKey(KeyCode.A) && (kecepatan > -kecepatanMaks))
        {
            kecepatan = kecepatan - Akselerasi * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) && (kecepatan < kecepatanMaks))
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
        if (Input.GetKey(KeyCode.S))
        {
            bisaJongkok = true;
        }
        else
        {
            //bisaJongkok = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            bisaJongkok = false;
        }

    }

    void GrabObject()
    {
        if (detect.grabableObject != null && tanganKiriIK != null)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E))
            {
                tanganKiriTerluka.enabled = false;
                tanganKiri.SetActive(true);
                isGrabbing = true;

                tanganKiriIK.parent = detect.grabableObject.transform.GetChild(0);
                detect.grabableObject.parent = this.transform;
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.E))
            {
                tanganKiriTerluka.enabled = true;
                tanganKiri.SetActive(false);
                isGrabbing = false;

                tanganKiriIK.parent = this.transform;
                detect.grabableObject.parent = null;
                //Destroy(GetComponent<HingeJoint2D>());
            }
        }

        if (detect.grabableObject == null)
        {
            tanganKiriTerluka.enabled = true;
            tanganKiri.SetActive(false);
            tanganKiriIK.parent = this.transform;
            isGrabbing = false;
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
        kecepatan = 3f;
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
}