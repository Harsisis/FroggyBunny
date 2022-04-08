using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBird : MonoBehaviour
{
    public Rigidbody2D rbBird;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCave"))
        {
            {
                StartCoroutine(Countdown());
            }
        }
    }

    private IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
