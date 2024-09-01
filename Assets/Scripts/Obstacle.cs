using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

// Obstacle class that inherits from MonoBehaviour, used for managing obstacles in the game
public class Obstacle : MonoBehaviour
{
    // OnTriggerEnter2D method is called when another 2D collider enters this trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the collision is with an object tagged "Border" or "Player"   
        if (collision.tag == "Border" || collision.tag == "Player")
        {
            // Destroys this object (obstacle)
            Destroy(this.gameObject);
        }
    }
}
