using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerCollision class that inherits from MonoBehaviour, used for detecting player collisions
public class PlayerCollision : MonoBehaviour
{
    // Public static field that keeps track of the player's number of lives
    public static int lifeNumb = 3;
    private GameObject obstacle; // Private field for reference to an obstacle
    public GameObject heart1; // Public field for heart 1
    public GameObject heart2; // Public field for heart 2
    public GameObject heart3; // Public field for heart 3

    AudioManager audioManager; // Private field for AudioManager

    // Awake method is called during the object's initialization
    private void Awake()
    {
        // Finds and sets reference to AudioManager using its tag
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start method is called when the script begins to execute
    void Start()
    {
        // Finds and sets reference to an obstacle
        obstacle = GameObject.FindGameObjectWithTag("Obstacle");
    }

    // OnTriggerEnter2D method is called when another 2D collider enters this trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if the collision is with an object tagged "Obstacle"
        if (collision.tag == "Obstacle")
        {
            // Reduces the player's number of lives by one
            lifeNumb--;

            // Plays the sound effect for hitting an obstacle
            audioManager.PlaySFX(audioManager.obstacleBop);

            // Checks if the number of lives is zero
            if (lifeNumb == 0)
            {
                // Hides the display of heart 1
                heart1.SetActive(false);

                // Destroys the player
                Destroy(this.gameObject);

                // Resets the number of lives to 3
                lifeNumb = 3;
            }

            // Checks if the number of lives is 2
            if (lifeNumb == 2)
            {
                // Hides the display of heart 3
                heart3.SetActive(false);
            }
            // Checks if the number of lives is 1
            else if (lifeNumb == 1)
            {
                // Hides the display of heart 2
                heart2.SetActive(false);
            }
        }
        // Checks if the collision is with an object tagged "Heart"
        else if (collision.tag == "Heart")
        {
            // Plays the sound effect for collecting a heart
            audioManager.PlaySFX(audioManager.heartCollect);

            // Checks if the number of lives is less than 3
            if (lifeNumb < 3)
            {
                // Increases the number of lives by one
                lifeNumb++;

                // If the number of lives is 3, shows heart 3
                if (lifeNumb == 3)
                {
                    heart3.SetActive(true);
                }
                // If the number of lives is not 3, checks if it is 2
                else if (lifeNumb == 2)
                {
                    // Shows heart 2
                    heart2.SetActive(true);
                }
            }
        }
    }
}
