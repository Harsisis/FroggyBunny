using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMur : MonoBehaviour
{
    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}