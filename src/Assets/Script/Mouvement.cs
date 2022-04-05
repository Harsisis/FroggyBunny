using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public bool isJumping;
    public bool canDoubleJump;

    public bool isGrounded;

    public bool isGroundedInSlimeZone;
    

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    public static Mouvement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scene");
            return;
        }

        instance = this;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                canDoubleJump = true;
                jump(canDoubleJump);
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                jump(canDoubleJump);
            }
        }

        FLip(rb.velocity.x);

        // set velocity params for animator
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void jump(bool canDoubleJump)
    {
        animator.SetBool("isJumping", true);

        if (canDoubleJump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        } 
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce * 1.2f));
        }
        
    }

    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);
        animator.SetBool("isJumping", !isGrounded);
        MovePlayer(horizontalMovement);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    }

    void FLip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void ChangeMoveSpeedAndJumpForce(float newMoveSpeed, float newJumpForce)
    {
        moveSpeed = newMoveSpeed;
        jumpForce = newJumpForce;
    }
}