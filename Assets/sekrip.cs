using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sekrip : MonoBehaviour
{
    [SerializeField]private Transform tangan;
    //public Transform target;
    Rigidbody2D rb;
    Animator anim;
        float speed = 3f;

    public float move;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (!Mathf.Approximately(0, move))
        {
            transform.rotation = -move > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        anim.SetFloat("Speed", Mathf.Abs(move * speed));
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 10f;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 3f;
        }

        move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isJump");
            rb.AddForce(new Vector2(rb.velocity.x, 200));
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("isCrouch", true);
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("isCrouch", false);
        }
    }
}
