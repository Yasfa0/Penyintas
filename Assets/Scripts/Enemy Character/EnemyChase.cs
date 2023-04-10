using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    Animator anim;
    PlayerCharacter player;
    [SerializeField] private float speed = 5;
    bool canMove = true;
    Vector2 move;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerCharacter>();
    }

    private void Update()
    {
        if (canMove)
        {
            transform.rotation = -move.x > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

            anim.SetTrigger("lari");
            move = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.position = move;
        }
    }
}
