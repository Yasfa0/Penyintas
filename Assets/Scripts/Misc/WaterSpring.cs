using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WaterSpring : MonoBehaviour
{
    public float velocity = 0f;
    float force = 0f;
    public float height = 0f;
    float target_height = 0f;
    int waveIndex;
    SpriteShapeController spriteShapeController;
    float resistance = 30f;

    public void Init(SpriteShapeController ssc)
    {
        int index = transform.GetSiblingIndex();
        waveIndex = index + 1;
        spriteShapeController = ssc;

        velocity = 0;
        height = transform.localPosition.y;
        target_height = transform.localPosition.y;
    }

    public void WavePointUpdate()
    {
        if (spriteShapeController != null)
        {
            Spline waterSpline = spriteShapeController.spline;
            Vector3 wavePosition = waterSpline.GetPosition(waveIndex);
            waterSpline.SetPosition(waveIndex, new Vector3(wavePosition.x,transform.localPosition.y,wavePosition.z));
        }
    }

    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        height = transform.localPosition.y;
        float x = height - target_height;
        float loss = -dampening * velocity;

        force = - springStiffness * x + loss;
        velocity += force;
        float y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x,y+velocity,transform.localPosition.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 speed = rb.velocity;

            velocity += speed.y / resistance;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 speed = rb.velocity;

            velocity += speed.y / resistance;
        }
    }

}
