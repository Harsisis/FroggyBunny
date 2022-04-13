using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;



public class DeathZone : MonoBehaviour
{

    private PlayerHealth PlayerHealthInstance;

    private void Start()
    {
        PlayerHealthInstance = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealthInstance.showMenu = false;
            PlayerHealthInstance.Die();
            yield return new WaitForSeconds(2f);
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
            GameOverManager.instance.RetryButton();
            PlayerHealthInstance.showMenu = true;
        }
    }

}
