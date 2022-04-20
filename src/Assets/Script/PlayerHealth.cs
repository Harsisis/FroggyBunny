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

    public HealthBar healthBar;

<<<<<<< HEAD
    public float invicibilityTimeAfterHit = 1f;
    public bool showMenu = true;
=======
    public float invicibilityTimeAfterHit = 3f;
>>>>>>> 58ce8d1292d3774c85641c0091c6994f9a7da539

    public static PlayerHealth instance;

    private Transform playerSpawn;
    private Animator fadeSystem;


    private PlayerHealth PlayerHealthInstance;
    private ReloadPosition ReloadPosition;
    private GameObject[] resetOnDeath;
    private float initialZoom;
    private GameObject Player;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la sc�ne");
            return;
        }

        instance = this;

        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        ReloadPosition = GameObject.Find("GameManager").GetComponent<ReloadPosition>();
        resetOnDeath = GameObject.FindGameObjectsWithTag("ResetOnDeath");
        initialZoom = GameObject.FindGameObjectWithTag("ResetOnDeath").GetComponent<Zoom>().getTargetOrtho;
        Player = GameObject.Find("Player");
    }

    void Start()
    {
        currentHealth = maxHealth;
<<<<<<< HEAD
        healthBar.SetHealth(currentHealth);
=======
        healthBar.SetMaxHealth(maxHealth);
>>>>>>> 58ce8d1292d3774c85641c0091c6994f9a7da539
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

            //V�rifier si le joueur est toujours vivant
            if(currentHealth <=0)
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
<<<<<<< HEAD
        if (showMenu)
        {
            GameOverManager.instance.OnPlayerDeath();
        }
=======
        GameOverManager.instance.OnPlayerDeath();
>>>>>>> 58ce8d1292d3774c85641c0091c6994f9a7da539
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

        foreach(GameObject toReset in resetOnDeath)
        {
            toReset.TryGetComponent<Zoom>(out Zoom zoom);

            if (zoom != null)
            {
                zoom.toogleAccessor = false;
                zoom = null;
            }
        }

        // Reset camera 

        Camera.main.orthographicSize = initialZoom;

        // Remise en place des physiques du jeu

        Mouvement.instance.playerCollider.enabled = true;
        Mouvement.instance.enabled = true;
        Mouvement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
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
