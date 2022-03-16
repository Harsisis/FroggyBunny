using System.Collections;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    #region D�claration des varibles et gameObject
    private float jumpHeight;
    private float playerPositionZero;
    private float mushPositionZero;
    private bool checkIfPlayerIsLeftOfMushs;
    private bool isGrounded;
    private Transform player;
    public Transform groundCheck;
    public Rigidbody2D enemyRB;
    public SpriteRenderer graphics;
    public GameObject _player;
    public GameObject _groundCheck;
    public Vector2 boxSize;
    #endregion

    // Le d�marrage est appel� avant la premi�re mise � jour de la trame
    void Start()
    {
        jumpHeight = 4.00f;
        player = GameObject.Find(_player.name).transform;
        groundCheck = GameObject.Find(_groundCheck.name).transform;
        StartCoroutine("TimerJumpAttack");
    }

    // La mise � jour est appel�e une fois par image
    void FixedUpdate()
    {
        // R�cup�ration de l'�tat du groundCheck de Player
        isGrounded = Physics2D.OverlapArea(groundCheck.position, boxSize);

        CheckIfPlayerIsLeft();
    }

    // V�rification de la position du Player
    void CheckIfPlayerIsLeft()
    {
        // Mise en place des positions fictives de Player et Mush
        playerPositionZero = player.position.x + 111.08f;
        mushPositionZero = transform.position.x + 111.088015f;

        // Si la position du Player est supp�rieure � celle de Mush
        if (playerPositionZero > mushPositionZero)
        {
            // Si le Player n'est pas � gauche de Mush
            if (checkIfPlayerIsLeftOfMushs == false)
            {
                // Rotation de Mush � 180�
                graphics.flipX = !graphics.flipX;
                checkIfPlayerIsLeftOfMushs = true;
            }
        }
        // Si la position du Player est inf�rieure � celle de Mush
        else if (playerPositionZero < mushPositionZero)
        {
            // Si le Player est � gauche de Mush
            if (checkIfPlayerIsLeftOfMushs == true)
            {
                // Rotation de Mush � 180�
                graphics.flipX = !graphics.flipX;
                checkIfPlayerIsLeftOfMushs = false;
            }
        }
    }

    // Attaque du Mush
    void EnnemyJumpAttack()
    {
        // R�cup�ration de la distance entre Mush et Player
        float distanceFromPlayer = player.position.x - transform.position.x;

        // Si la distance entre Mush et Player est inf�rieure � 2
        if (Vector2.Distance(player.position, transform.position) < 2)
        {
            // Si Mush touche le sol
            if (isGrounded)
            {
                // Application d'une force sur le saut de Mush
                enemyRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
            }
        }
    }

    // Toutes les 3 secondes, il ex�cute la fonction EnnemyJumpAttack
    IEnumerator TimerJumpAttack()
    {
        while (true)
        {
            EnnemyJumpAttack();
            yield return new WaitForSeconds(2);
        }
    }
}
