using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isInvincible = false;

    public SpriteRenderer graphics;

    public float invinciblityFlashDelay = 0.2f;

    public HealthBar healthBar;

    public float invicibilityTimeAfterHit = 3f;
    public bool showMenu = true;

    public static PlayerHealth instance;

    private Transform playerSpawn;
    private Animator fadeSystem;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;

        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(100);
        }
    }

    public void HealPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible) 
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            //Vérifier si le joueur est toujours vivant
            if(currentHealth <=0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }       
    }

    public void Die()
    {
        Mouvement.instance.enabled = false;
        Mouvement.instance.animator.SetTrigger("Die");
        Mouvement.instance.rb.bodyType = RigidbodyType2D.Static;
        Mouvement.instance.playerCollider.enabled = false;
        StopAllCoroutines();
        if (showMenu)
        {
            GameOverManager.instance.OnPlayerDeath();
        }
    }

    public void Respawn()
    {
        Mouvement.instance.enabled = true;
        Mouvement.instance.animator.SetTrigger("Respawn");
        Mouvement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        Mouvement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator InvincibilityFlash()
    {
        while(isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invinciblityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invinciblityFlashDelay);
        }

     
        }
    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvincible = false;
    }
}
