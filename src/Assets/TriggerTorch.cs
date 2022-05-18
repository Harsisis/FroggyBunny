using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTorch : MonoBehaviour
{
    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 2f;
        }
    }
}
