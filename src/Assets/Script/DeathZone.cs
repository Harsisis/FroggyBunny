using UnityEngine;
using System.Collections;
using System.Data;
using System.Collections.Generic;

public class DeathZone : MonoBehaviour
{
    private PlayerHealth PlayerHealthInstance;
    private Transform playerSpawn;
    private ReloadPosition ReloadPosition;
    private Zoom zoom;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        PlayerHealthInstance = GameObject.Find("Player").GetComponent<PlayerHealth>();
        ReloadPosition = GameObject.Find("GameManager").GetComponent<ReloadPosition>();
        zoom = GameObject.FindGameObjectWithTag("ResetOnDeath").GetComponent<Zoom>();
    }
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealthInstance.showMenu = false;
            PlayerHealthInstance.Die();
            yield return new WaitForSeconds(0.6f);
            PlayerHealthInstance.Respawn();
            PlayerHealthInstance.showMenu = true;
        }
    }
}
