using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static bool canMove = true;

    Rigidbody2D rb;
    Animator anim;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpStrength = 200f;
    float move;

    // Start is called before the first frame update
    void Start()
    {
        //baseSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !PauseMenu.Instance.GetIsPaused())
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
        }
        
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        speed = 0;
        anim.SetFloat("Speed", Mathf.Abs(move * speed));
        Debug.Log("Movement Stopped");
    }

    public void ResetSpeed()
    {
        speed = 3f;
        anim.SetFloat("Speed", Mathf.Abs(move * speed));
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
