using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damageOnCollision = 20;

    public bool activedTheDamageOption = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (activedTheDamageOption)
            {
               GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(damageOnCollision);
            }
        }
    }
}
