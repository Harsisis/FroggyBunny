using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private PlayerHealth PlayerHealthInstance;

    private void Awake()
    {
        PlayerHealthInstance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerHealthInstance.DieAndRespawnWithoutMenu());
        }
    }
}
