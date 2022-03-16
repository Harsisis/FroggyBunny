using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.active = false;
        }
    }
}
