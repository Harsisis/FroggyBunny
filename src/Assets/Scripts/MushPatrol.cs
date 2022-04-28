using UnityEngine;

public class MushPatrol : MonoBehaviour
{
    #region Déclaration des varibles et gameObject
    private int destPoint;
    private int damageOnCollision;
    private float speed;
    private bool checkDistance;
    private Transform target;
    private Transform player;
    public GameObject _player;
    public Transform[] waypoints;
    public SpriteRenderer graphics;
    #endregion

    // Le démarrage est appelé avant la première mise à jour de la trame
    void Start()
    {
        damageOnCollision = 20;
        speed = 1.00f;
        target = waypoints[0];
        checkDistance = false;
        player = GameObject.Find(_player.name).transform;
    }

    // La mise à jour est appelée une fois par image
    void Update()
    {
        // Récupération de la direction
        Vector2 dir = target.position - transform.position;

        // // Si la distance entre Mush et Player est inférieur à 2
        if (Vector2.Distance(player.position, transform.position) < 2)
        {
            // Ils sont pas proche
            checkDistance = true;
        }
        else
        {
            // Ils ne sont proche
            checkDistance = false;
        }

        // Si Mush et Player son proche
        if (checkDistance == true)
        {
            // Changement de la position de mouvement de Mush pour suivre Player
            transform.position = Vector2.MoveTowards(transform.position, player.position, 0.5f * Time.deltaTime);
        }
        else
        {
            // Déplacement de Mush vers l'avant sur l'axe x
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            //Si Mush est quasiment arrivé au waypoint suivant
            if (Vector2.Distance(transform.position, target.position) < 0.3f)
            {
                // Calcul du prochaine waypoint de destination pour Mush
                destPoint = (destPoint + 1) % waypoints.Length;
                // Changement de waypoint de destination
                target = waypoints[destPoint];
            }
        }
    }

    // Détection d'une collision entre deux objets
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si il y a une collision entre Mush et Player
        if (collision.transform.CompareTag(_player.name))
        {
            // Le player subira 20 de dommage
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
