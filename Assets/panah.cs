using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panah : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            rb.AddForce(Vector2.right * 2000);
        }
    }
}
