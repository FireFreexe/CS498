using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float speed = 1f; 
    [SerializeField] private float initialKick = 1f; 
    
    private Vector2 inputDirection; 
    private Vector2 slidingDirection; 
    private Rigidbody2D rb;
    public Animator animator; //new
    public SpriteRenderer sprite; //new

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>(); //new
    }

    public void OnMovement(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    private void Update()
    {
        if (slidingDirection == Vector2.zero && inputDirection.sqrMagnitude > 0.01f)
        {
            slidingDirection = GetCardinalDirection(inputDirection);

            rb.AddForce(slidingDirection * initialKick, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (slidingDirection != Vector2.zero)
        {
            rb.AddForce(slidingDirection * speed);
        }
        //Takes absolute value of speed and passes paramater to animator
        animator.SetFloat("Velocity", Mathf.Abs(rb.linearVelocity.x)); //new
        //Flip sprite depending on input direction
        if (slidingDirection.x < 0)
            sprite.flipX = true;
        else if (slidingDirection.x > 0)
            sprite.flipX = false;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckWallHit(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckWallHit(collision);
    }

    private void CheckWallHit(Collision2D collision)
    {
        if (slidingDirection == Vector2.zero) return;

        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 wallNormal = collision.GetContact(i).normal;

            if (Vector2.Dot(slidingDirection, wallNormal) < -0.8f) 
            {
                slidingDirection = Vector2.zero;
                rb.linearVelocity = Vector2.zero; 
                break;
            }
        }
    }

    private Vector2 GetCardinalDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return new Vector2(Mathf.Sign(input.x), 0f);
        return new Vector2(0f, Mathf.Sign(input.y));
    }
}