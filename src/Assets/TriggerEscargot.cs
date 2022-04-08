using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEscargot : MonoBehaviour
{
    public Rigidbody2D rbEscargot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCave"))
        {
            {
                gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
