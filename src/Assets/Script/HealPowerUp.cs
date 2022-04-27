using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int healthPoints;
    private PlayerHealth playerHealthInstance = GameObject.Find("Player").GetComponent<PlayerHealth>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (playerHealthInstance.currentHealth != playerHealthInstance.maxHealth)
            {
                playerHealthInstance.HealPlayer(healthPoints);
                Destroy(gameObject);
            }
            
        }
    }
}
