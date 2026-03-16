using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    //Script from the following video:
    //https://www.youtube.com/watch?v=xrLlZ1mHCTA

    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement (InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    private void FixedUpdate()
    {
        rb.AddForce(movement * speed);
    }
}