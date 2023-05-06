using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    private Transform player;
    //public Transform bg1;
    public Transform startLimit;
    public Transform endLimit;
    private bool canFollow = true;

    private void Awake()
    {
        if (FindObjectOfType<PlayerCharacter>())
        {
            player = FindObjectOfType<PlayerCharacter>().transform;
        }
    }

    private void Start()
    {
        if (!Vector2.Equals(PlayerData.GetSpawnPosition(), Vector2.zero))
        {
            if (PlayerData.GetSpawnPosition().x >= endLimit.position.x)
            {
                transform.position = new Vector3(endLimit.position.x,transform.position.y,transform.position.z);
            }
        }
    }

    private void Update()
    {
        if (player && canFollow)
        {
            if (player.position.x != transform.position.x && player.position.x > startLimit.position.x && player.position.x < endLimit.position.x)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 1f);
                //bg1.transform.position = new Vector2(transform.position.x * 1.0f, bg1.transform.position.y);
            }
        }
    }

    public void SetCanFollow(bool inputValue)
    {
        canFollow = inputValue;
    }
}
