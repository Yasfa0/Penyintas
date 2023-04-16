using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStatic : MonoBehaviour
{
    public Transform[] batas;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!spriteRenderer.flipX)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }

        if(transform.position.x >= batas[1].position.x && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if(transform.position.x <= batas[0].position.x && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }

    void MoveRight()
    {
        Vector3 pos = transform.position;
        pos.x += 1 * Time.deltaTime;
        transform.position = pos;
        if(spriteRenderer.flipX )
        {
            spriteRenderer.flipX = false;
        }
    }

    void MoveLeft()
    {
        Vector3 pos = transform.position;
        pos.x -= 1 * Time.deltaTime;
        transform.position = pos;
        if(!spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
    }
}
