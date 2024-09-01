using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Heart class that inherits from MonoBehaviour, used for managing the behavior of hearts in the game
public class Heart : MonoBehaviour
{
    private GameObject player; // Private field for reference to the player

    // Start method is called when the script begins to execute
    void Start()
    {
        // Finds and sets reference to the object with tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // OnTriggerEnter2D method is called when another 2D collider enters this trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the collision is with an object tagged "Border" or "Player"
        if (collision.tag == "Border" || collision.tag == "Player")
        {
            // Destroys this object (heart)
            Destroy(this.gameObject);
        }
    }
}
