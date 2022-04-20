using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMur : MonoBehaviour
{
    public Rigidbody2D rbMur;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escargot"))
        {
            {
                gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
