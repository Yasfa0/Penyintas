using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Attribute")]
    Rigidbody2D rb;
    Animator anim;
    Transform tanganKiriIK;
    public bool Injured;
    [Space(10)]

    [Header("Character Speed with Acceleration System")]
    float kecepatan = 0f;
    public float InjuredMaxSpeed = 1f;
    public float HealthyMaxSpeed = 3.5f; 
    public float kecepatanMaks;
    public float RunMaxSpeed = 8f;
    public float Akselerasi = 5f;
    public float Deselerasi = 10f;
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
    bool lagiLoncat;
    public bool bisaLoncat;
    float gravity;
    float kecepatanAwal;
    bool kenaAir;
    bool diam;
        float t = 0;

    float move;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        detect = GetComponentInChildren<DetectGrab>();
        tanganKiriIK = transform.Find("Tangan Kiri");
        
    }

    private void Start()
    {
        playerDead = false;
        isGrabbing = false;
        bisaLoncat = true;
        lagiLoncat = false;
        gravity = rb.gravityScale;
        kecepatanAwal = kecepatanMaks;
        diam = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(t);

        anim.SetBool("isDead", playerDead); // animasi player mati
        
        if(!playerDead)
        {
            //if(!isGrabbing)
            {
                // Flip Character
                if (!Mathf.Approximately(0, kecepatan))
                {
                    transform.rotation = -kecepatan > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
                    if(Injured)
                    {
                        tanganKananTerluka.sortingOrder = 33;
                        tanganKiriTerluka.sortingOrder = 23;
                        if (transform.rotation == Quaternion.Euler(0, 180, 0))
                        {
                            tanganKananTerluka.sortingOrder = 23;
                            tanganKiriTerluka.sortingOrder = 33;
                        }
                    }
                }
            }
            PlayerMovement();
            GrabObject();
            PlayerCheck(Injured);
            if(kecepatan == 0)
            {
                if(t < 20)                      // animasi idle jika player diam dalam jangka waktu tertentu
                {                               
                    t += Time.deltaTime;
                }
                else
                {
                    anim.SetTrigger("isLong");
                    t = 0;
                }
            }
            else
            {
                t = 0;
                anim.ResetTrigger("isLong");
            }

            if(Input.GetKeyDown(KeyCode.W) && bisaLoncat)
            {
                bisaLoncat = false;
                lagiLoncat = true;
            }
                anim.SetBool("isJump", lagiLoncat);
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, 200f));
    }

    void PlayerCheck(bool check) // untuk check player apakah keadaan sedang injured atau healthy
    {
        if (check)
        {
            anim.SetTrigger("isInjured"); // animasi
            anim.ResetTrigger("Healthy");

            tanganKananTerluka.enabled = true; // sprite renderer
            tanganKiriTerluka.enabled = true;
            tanganKiri.SetActive(false);
            tanganKanan.enabled = false;

            if (kecepatanMaks != InjuredMaxSpeed)
                anim.SetFloat("kecepatan", kecepatanMaks - 0.3f); // kecepatan animasi jika kecepatan player dinaikkan
            else
                anim.SetFloat("kecepatan", 1);
        }

        if (!check)
        {
            anim.SetTrigger("Healthy"); // animasi
            anim.ResetTrigger("isInjured");

            tanganKananTerluka.enabled = false; // sprite renderer
            tanganKiriTerluka.enabled = false;
            tanganKiri.SetActive(true);
            tanganKanan.enabled = true;

            if (kecepatanMaks != HealthyMaxSpeed)
                anim.SetFloat("kecepatan", kecepatanMaks - 0.3f); // kecepatan animasi jika kecepatan player dinaikkan
            else
                anim.SetFloat("kecepatan", 1);
        }
    }

    void PlayerMovement()
    {
        // Movement Animation
        anim.SetFloat("speed", Mathf.Abs(kecepatan));
        anim.SetBool("canCrouch", bisaJongkok);

        // Gerak Kiri Kanan
        if (Input.GetKey(KeyCode.A))
        {
            diam = false;
            if (kecepatan > Deselerasi * Time.deltaTime)
            {
                kecepatan = kecepatan - Deselerasi * Time.deltaTime;
            }
            if (!Injured)
            {
                if (bisaJongkok)
                {
                    kecepatanMaks = InjuredMaxSpeed;
                    kecepatan = -kecepatanMaks;
                }
                else
                    if(!kenaAir)
                    kecepatanMaks = HealthyMaxSpeed;

                if(Input.GetKey(KeyCode.LeftShift) && (kecepatan > -RunMaxSpeed) && !bisaJongkok)
                    kecepatan = kecepatan - Akselerasi * Time.deltaTime;
                else if(Input.GetKeyUp(KeyCode.LeftShift))
                {
                    kecepatan = -kecepatanMaks;
                }
                else
                {
                    if(kecepatan > -kecepatanMaks)
                        kecepatan = kecepatan - Akselerasi * Time.deltaTime;
                }
            }

            if(Injured)
            {
                kecepatanMaks = InjuredMaxSpeed;
                if (kecepatan > -kecepatanMaks)
                    kecepatan = kecepatan - Akselerasi * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            diam = false;
            if (kecepatan < -Deselerasi * Time.deltaTime)
            {
                kecepatan = kecepatan + Deselerasi * Time.deltaTime;
            }
            if (!Injured)
            {
                if (bisaJongkok)
                {
                    kecepatanMaks = InjuredMaxSpeed;
                    kecepatan = kecepatanMaks;
                }
                else
                    if(!kenaAir)
                    kecepatanMaks = HealthyMaxSpeed;

                if (Input.GetKey(KeyCode.LeftShift) && (kecepatan < RunMaxSpeed) && !bisaJongkok)
                    kecepatan = kecepatan + Akselerasi * Time.deltaTime;
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    kecepatan = kecepatanMaks;
                }
                else
                {
                    if (kecepatan < kecepatanMaks)
                        kecepatan = kecepatan + Akselerasi * Time.deltaTime;
                }
            }

            if(Injured)
            {
                kecepatanMaks = InjuredMaxSpeed;
                if (kecepatan < kecepatanMaks)
                    kecepatan = kecepatan + Akselerasi * Time.deltaTime;
            }
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
                diam = true;
            }
        }
        transform.position = new Vector2(transform.position.x + kecepatan * Time.deltaTime, transform.position.y);
        
        // Jongkok
        if(Input.GetKey(KeyCode.S))
        {
            bisaJongkok = true;
            kecepatanMaks = InjuredMaxSpeed;
        }
        else
        {
            //bisaJongkok = false;
        }
        
        if(Input.GetKeyUp(KeyCode.S))
        {
            bisaJongkok = false;
            if (Injured)
                kecepatanMaks = InjuredMaxSpeed;
            else
                kecepatanMaks = HealthyMaxSpeed;
        }

    }

    void GrabObject()
    {
        if(Injured)
        {
            if(detect.grabableObject != null && tanganKiriIK != null)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    tanganKiriTerluka.enabled = false;
                    tanganKiri.SetActive(true);
                    isGrabbing = true;

                    tanganKiriIK.parent = detect.grabableObject.transform.GetChild(0);
                    detect.grabableObject.parent = this.transform;
                }

                if(Input.GetKeyUp(KeyCode.Space))
                {
                    tanganKiriTerluka.enabled = true;
                    tanganKiri.SetActive(false);
                    isGrabbing = false;

                    tanganKiriIK.parent = this.transform;
                    detect.grabableObject.parent = null;
                }
               
            }

            if(detect.grabableObject == null)
            {
                tanganKiriTerluka.enabled = true;
                tanganKiri.SetActive(false);
                tanganKiriIK.parent = this.transform;
                isGrabbing = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Water")
        {
            anim.speed = 0.5f;
        }

        if(collision.collider.tag == "Ground")
        {
            bisaLoncat = true;
            lagiLoncat = false;
        }
        else
        {
            lagiLoncat = false;
            bisaLoncat = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            bisaLoncat = false;
            lagiLoncat = false;
        }

        if(collision.collider.tag == "Water")
        {
            anim.speed = 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "Water")       // jika player masuk lumpur 
        {
            Debug.Log(kecepatan);
            rb.gravityScale = 0.1f;
            //anim.speed = 0.5f;
            if(Input.GetKey(KeyCode.D) && kecepatanMaks > 0.1f)
            {
                kecepatanMaks -= 0.01f;
            }
            if(Input.GetKey(KeyCode.A) && kecepatanMaks < kecepatanAwal)
            {
                kecepatanMaks += 0.1f;
            }
            kenaAir = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Water")       // jika player keluar lumpur
        {
            rb.gravityScale = gravity;
            anim.speed = default;
            kenaAir = false;
            anim.speed = 1;
        }       
    }
}
