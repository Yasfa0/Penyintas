using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private int contactDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerCharacter>())
        {
            collision.gameObject.GetComponent<PlayerCharacter>().TakeDamage(contactDamage);
        }
    }
}
