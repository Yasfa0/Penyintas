using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSight : MonoBehaviour
{
    private Vector2 spawnPosition;
    [SerializeField] private float patrolRange = 3f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitDuration = 1f;
    private List<Vector2> posTarget = new List<Vector2>();
    private int targetIndex;
    private bool canMove = true;

    private void Awake()
    {
        spawnPosition = transform.position;
        posTarget.Add(spawnPosition + (Vector2.right * patrolRange));
        posTarget.Add(spawnPosition - (Vector2.right * patrolRange));
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, posTarget[targetIndex], speed * Time.deltaTime);
        }

        if (Mathf.Approximately(transform.position.x,posTarget[targetIndex].x))
        {
            StartCoroutine(PauseMovement());
            if (targetIndex >= posTarget.Count - 1)
            {
                targetIndex = 0;
            }
            else
            {
                targetIndex++;
            }
        }
    }

    public IEnumerator PauseMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(waitDuration);
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Detected");
        }
    }
}
