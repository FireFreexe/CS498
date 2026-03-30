using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    //Script from the following video:
    //https://www.youtube.com/watch?v=xrLlZ1mHCTA

    private Vector2 movementDirection;
    private bool canChooseDirection = true;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnMovement(InputValue value)
    {
        if (!canChooseDirection)
        {
            return;
        }

        Vector2 input = value.Get<Vector2>();
        if (input.sqrMagnitude < 0.01f)
        {
            return;
        }

        movementDirection = GetCardinalDirection(input);
        canChooseDirection = false;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = movementDirection * speed;

        if (movementDirection.x != 0f)
        {
            velocity.y = 0f;
        }
        else if (movementDirection.y != 0f)
        {
            velocity.x = 0f;
        }

        rb.linearVelocity = velocity;
        rb.angularVelocity = 0f;
        rb.rotation = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (movementDirection == Vector2.zero)
        {
            return;
        }

        bool hitBlockingWall = false;
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 wallNormal = collision.GetContact(i).normal;

            // Stop only when the wall faces against our movement direction.
            if (Vector2.Dot(movementDirection, wallNormal) < -0.5f)
            {
                hitBlockingWall = true;
                break;
            }
        }

        if (!hitBlockingWall)
        {
            return;
        }

        movementDirection = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        canChooseDirection = true;
    }

    private static Vector2 GetCardinalDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            return new Vector2(Mathf.Sign(input.x), 0f);
        }

        return new Vector2(0f, Mathf.Sign(input.y));
    }
}
