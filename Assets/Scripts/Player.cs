using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player class that inherits from MonoBehaviour, used for managing the player in the game
public class Player : MonoBehaviour
{
    // Public field for the player's movement speed
    public float playerSpeed;

    private Rigidbody2D rb; // Private field for the player's Rigidbody component
    private Vector2 playerDirection; // Private field for the player's movement direction

    // Start method is called when the script begins to execute
    void Start()
    {
        // Finds and sets reference to the player's Rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update method is called once per frame
    void Update()
    {
        // Reads input for the vertical axis (up and down arrows)
        float directionY = Input.GetAxisRaw("Vertical"); // if down arrow, it will be -1, if up arrow, it will be 1
        // Sets the player's direction as a normalized vector with the y component from the input
        playerDirection = new Vector2(0, directionY).normalized;
    }

    // FixedUpdate method is called at a fixed time step
    void FixedUpdate()
    {
        // Sets the player's velocity based on the movement direction and speed
        rb.velocity = new Vector2(0, playerDirection.y * playerSpeed);
    }
}
