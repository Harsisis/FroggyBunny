using UnityEngine;
using System.Collections;
using System.Data;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public float invinciblityFlashDelay = 0.1f;
    public float invicibilityTimeAfterHit = 1f;
    public bool showMenu = true;
    
    private HealthBar healthBar;
    private Transform playerSpawn;
    private Animator fadeSystem;
    private ReloadPosition ReloadPosition;
    private GameObject[] resetOnDeath;
    private float initialZoom;
    private GameObject Player;
    private ScoreManager scoreManager;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        ReloadPosition = GameObject.Find("GameManager").GetComponent<ReloadPosition>();
        resetOnDeath = GameObject.FindGameObjectsWithTag("ResetOnDeath");
        healthBar = GameObject.Find("HealthBarCanvas").GetComponent<HealthBar>();
        initialZoom = Camera.main.orthographicSize;
        Player = GameObject.Find("Player");
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(100);
        }
    }

    public void HealPlayer(int amount)
    {
        if ((currentHealth + amount) > maxHealth)
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

            //Verifier si le joueur est toujours vivant
            if (currentHealth <= 0)
            {
                showMenu = false;
                StartCoroutine(DieAndRespawnWithoutMenu());
                showMenu = true;
                return;
            }

            isInvincible = true;
            StartCoroutine(HandleInvincibilityDelay());
            StartCoroutine(InvincibilityFlash());
        }
    }

    public IEnumerator DieAndRespawnWithoutMenu()
    {
        showMenu = false;
        Die();
        yield return new WaitForSeconds(0.6f);
        Respawn();
        showMenu = true;
    }

    public void Die()
    {
        Mouvement.instance.enabled = false;
        Mouvement.instance.animator.SetTrigger("Die");
        Mouvement.instance.rb.bodyType = RigidbodyType2D.Static;
        Mouvement.instance.playerCollider.enabled = false;
        if (showMenu)
        {
            GameOverManager.instance.OnPlayerDeath();
            return;
        }
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        Mouvement.instance.animator.SetTrigger("Respawn");

        // Remise de la position sur le dernier respawn

        Player.transform.position = playerSpawn.position;

        // Reset de la position des éléments susceptibles de générer un softlock

        foreach (DataRow dr in ReloadPosition.getNamesAndPositions.Rows)
        {
            GameObject itemToPosition = GameObject.Find(dr["Name"].ToString());
            itemToPosition.TryGetComponent<Rigidbody2D>(out var rigidbody);

            if (rigidbody != null)
            {
                rigidbody.velocity = new Vector2(0, 0);
            }
            float posX = (float)dr["PositionX"];
            float posY = (float)dr["PositionY"];

            Vector2 position = new Vector2(posX, posY);
            itemToPosition.transform.position = position;
        }

        // Reset des élément possédant un Toogle et devant être remis à l'état initial

        foreach (GameObject toReset in resetOnDeath)
        {
            toReset.TryGetComponent<Zoom>(out Zoom zoom);

            if (zoom != null)
            {
                zoom.toogleAccessor = false;
                zoom = null;
            }
        }

        // Reset camera 

        if (initialZoom > 0f)
        {
            Camera.main.orthographicSize = initialZoom;
        }
 

        // Remise en place des physiques du jeu

        Mouvement.instance.playerCollider.enabled = true;
        Mouvement.instance.enabled = true;
        Mouvement.instance.rb.bodyType = RigidbodyType2D.Dynamic;

        // On applique le malus de point

        if (Inventory.instance.coinsCount > 0)
        {
            scoreManager.DieMalus();
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
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
