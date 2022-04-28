using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDamage : MonoBehaviour
{
    public int damageOnCollision = 20;
    public bool activedTheDamageOption;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerCave"))
        {
            if (activedTheDamageOption)
            {
                PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damageOnCollision);
            }
        }
    }
}
