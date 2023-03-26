using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (kecepatanMaks != 1)
            anim.SetFloat("kecepatan", kecepatanMaks - 0.3f); // kecepatan animasi jika kecepatan player dinaikkan
        else
            anim.SetFloat("kecepatan", 1);

        anim.SetBool("isDead", playerDead); // animasi player mati
        
        if(!playerDead)
        {
            if(!isGrabbing)
            {
                //Flip Character
                if (!Mathf.Approximately(0, kecepatan))
                {
                    transform.rotation = -kecepatan > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
                    tanganKananTerluka.sortingOrder = 33;
                    tanganKiriTerluka.sortingOrder = 23;
                    if (transform.rotation == Quaternion.Euler(0, 180, 0))
                    {
                        tanganKananTerluka.sortingOrder = 23;
                        tanganKiriTerluka.sortingOrder = 33;
                    }
                }
            }
            PlayerMovement();
            GrabObject();
        }
    }

    void PlayerMovement()
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
        if(Input.GetKey(KeyCode.S))
        {
            bisaJongkok = true;
        }
        else
        {
            //bisaJongkok = false;
        }
        
        if(Input.GetKeyUp(KeyCode.S))
        {
            bisaJongkok = false;
        }

    }

    void GrabObject()
    {
        if(detect.grabableObject != null && tanganKiriIK != null)
        {
            if(Input.GetKey(KeyCode.Space))
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
                //Destroy(GetComponent<HingeJoint2D>());
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
