using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ScoreManager class that inherits from MonoBehaviour, used for managing the score
public class ScoreManager : MonoBehaviour
{
    // Public Text component for displaying the current score
    public Text scoreText;

    // Public Text component for displaying the high score
    public Text highScore;

    // Field that holds information on whether the player has beaten the high score
    public int beatedHighScore;

    // Public field that holds the player's current score
    public float score;

    // Reference to the game over panel
    public GameObject gameOverPanel;

    // Start method is called when the script begins to execute
    void Start()
    {
        // Initializes the field that holds information on whether the player has beaten the high score
        beatedHighScore = 0;
        // Updates the text for the high score
        UpdateHighScoreText();
    }

    // Update method is called once per frame
    void Update()
    {
        // Checks if the game over panel is not active
        if (gameOverPanel != null && !gameOverPanel.activeSelf)
        {
            // Checks if the player still exists in the scene (Is alive)
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                // Increases the player's score over time
                score += Time.deltaTime;
                // Updates the text for the current score
                scoreText.text = ((int)score).ToString();
                // Checks if the high score has been beaten
                CheckHighScore();
            }
        }
    }

    // Method to check if the high score has been beaten
    void CheckHighScore()
    {
        // Checks if the current score is higher than the high score stored in PlayerPrefs
        if ((int)score > (int)PlayerPrefs.GetFloat("HighScore", 0))
        {
            // Sets flag that the high score has been beaten
            beatedHighScore = 1;

            // Saves the new high score in PlayerPrefs
            PlayerPrefs.SetFloat("HighScore", score);

            // Updates the text for the high score
            UpdateHighScoreText();
        }
    }
    // Method to update the text for the high score
    void UpdateHighScoreText()
    {
        // Sets the text to display the high score
        highScore.text = "HighScore: " + ((int)PlayerPrefs.GetFloat("HighScore", 0)).ToString();
    }
}
