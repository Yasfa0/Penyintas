using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    //private Vector2 startpos;
    float startpos;
    private float length;
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float xOffset;

    //Vector2 travel => (Vector2)cam.transform.position - startpos;

    private void Awake()
    {
        startpos = transform.position.x;
        //startpos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.gameObject;
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);


        transform.position = new Vector3(startpos + dist + xOffset, transform.position.y, transform.position.z);
        //Vector2 newPos = startpos + travel * parallaxEffect;
        //transform.position = new Vector2(newPos.x,newPos.y);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;

    }

}
