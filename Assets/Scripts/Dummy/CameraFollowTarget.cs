using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform player;
    //public Transform bg1;
    public Transform endLimit;

    private void Update()
    {
        if (player.position.x != transform.position.x && player.position.x > 0 && player.position.x < endLimit.position.x)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 0.1f);
            //bg1.transform.position = new Vector2(transform.position.x * 1.0f, bg1.transform.position.y);
        }
    }
}
