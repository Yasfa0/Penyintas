using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;
    private bool canMove = true;
    [SerializeField] private float speed = 5;
    private Animator animator;
    private static Vector2 spawnPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (SaveSystem.currentSaveData != null)
        {
            SaveData save = SaveSystem.currentSaveData;
            Vector2 tempPos = new Vector3(save.posX, save.posY);
            spawnPosition = tempPos;
        }

        if(spawnPosition != Vector2.zero)
        {
            transform.position = spawnPosition;
        }
    }

    private void Update()
    {
        if (canMove)
        {
            moveX = Input.GetAxisRaw("Horizontal") * speed;
            if(moveX != 0)
            {
                animator.SetBool("isWalking",true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if(moveX > 0 )
            {
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            else if(moveX < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            rb.velocity = new Vector2(moveX, rb.velocity.y);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            animator.SetBool("onGround",true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("onGround", false);
        }
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        animator.SetBool("isWalking",false);
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetSpawnPosition(Vector3 position)
    {
        spawnPosition = position;
    }

}
