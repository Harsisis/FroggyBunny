using UnityEngine;
<<<<<<< HEAD

public class DeathZone : MonoBehaviour
{
    private PlayerHealth PlayerHealthInstance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerHealthInstance.DieAndRespawnWithoutMenu());
        }
    }
=======
using System.Collections;

public class DeathZone : MonoBehaviour
{

    private Transform playerSpawn;
    private Animator fadeSystem;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision)); 
        }
    }

    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = playerSpawn.position;
    }
>>>>>>> 58ce8d1292d3774c85641c0091c6994f9a7da539
}
