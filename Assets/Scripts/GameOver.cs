using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// GameOver class that inherits from MonoBehaviour, used for managing game over logic
public class GameOver : MonoBehaviour
{
    // Public GameObjects set in the inspector for the game over and pause panels
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject pauseScore; // GameObject with Text component
    public GameObject gameOverScore; // GameObject with Text component
    private ScoreManager scoreManager; // Reference to ScoreManager
    private bool isPaused = false; // Bool value tracking whether the game is paused
    AudioManager audioManager; // Reference to AudioManager
    private bool soundPlayed = false; // Bool value tracking whether the game over sound has been played

    // Start method is called when the script begins to execute
    void Start()
    {
        // Finds and sets reference to ScoreManager
        scoreManager = FindObjectOfType<ScoreManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        soundPlayed = false;
    }
    
    // Update method is called once per frame
    void Update()
    {
        // Checks if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Calls method to toggle pause state
            if (GameObject.FindGameObjectWithTag("Player") != null)
                TogglePause();
        }

        // Checks if the player is "dead"
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            if (!soundPlayed)  // Plays sound if it hasn't been played yet
            {
                audioManager.PlaySFX(audioManager.gameOver);
                soundPlayed = true;
            }
            // Activates the game over panel
            gameOverPanel.SetActive(true);

            // Sets text to display the final score
            if (scoreManager != null)
            {
                // Finds TextMeshProUGUI component and sets the text
                TextMeshProUGUI gameOverTextMeshPro = gameOverScore.GetComponent<TextMeshProUGUI>();
                if (gameOverTextMeshPro != null)
                {
                    gameOverTextMeshPro.text = "Your score\n" + ((int)scoreManager.score).ToString();
                }
            }
        }
    }

    // Public method to restart the game
    public void Restart()
    {
        // Resets number of lives
        PlayerCollision.lifeNumb = 3;

        // Resets time scale to normal
        Time.timeScale = 1;

        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Public method to go back to the main menu
    public void BackToMainMenu()
    {
        // Resets number of lives
        PlayerCollision.lifeNumb = 3;

        // Resets time scale to normal
        Time.timeScale = 1;

        // Loads the main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Method to toggle pause state
    void TogglePause()
    {
        // Toggles pause state
        isPaused = !isPaused;

        // Activates or deactivates pause panel
        pausePanel.SetActive(isPaused);

        // Pauses or resumes the game
        Time.timeScale = isPaused ? 0 : 1;

        // Updates pause text if the game is paused
        if (isPaused)
        {
            // Updates score text during pause
            TextMeshProUGUI pauseTextMeshPro = pauseScore.GetComponent<TextMeshProUGUI>();
            if (pauseTextMeshPro != null)
            {
                pauseTextMeshPro.text = "Your score\n" + ((int)scoreManager.score).ToString();
            }
        }
    }
}
