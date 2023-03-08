using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asalsekri : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float kecepatan = 0f;
    public float kecepatanMaks = 1f;
    float Akselerasi = 2f;
    float Deselerasi = 10f;
    public SpriteRenderer tanganKanan;
    public SpriteRenderer tanganKiri;
    float move;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Mathf.Approximately(0, kecepatan))
        {
            transform.rotation = -kecepatan > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            tanganKanan.sortingOrder = 33;
            tanganKiri.sortingOrder = 8;
            if(transform.rotation == Quaternion.Euler(0, 180, 0))
            {
                tanganKanan.sortingOrder = 8;
                tanganKiri.sortingOrder = 33;
            }
        }

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
            if(kecepatan > Deselerasi * Time.deltaTime)
            {
                kecepatan = kecepatan - Deselerasi * Time.deltaTime;
            }
            else if(kecepatan < -Deselerasi * Time.deltaTime)
            {
                kecepatan = kecepatan + Deselerasi * Time.deltaTime;
            }
            else
            {
                kecepatan = 0;
            }
        }

        transform.position = new Vector2(transform.position.x + kecepatan * Time.deltaTime, transform.position.y);

        anim.SetFloat("speed", Mathf.Abs(kecepatan));
        
    }
}
