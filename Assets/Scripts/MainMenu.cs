using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// MainMenu class that inherits from MonoBehaviour, used for managing the main menu in the game
public class MainMenu : MonoBehaviour
{
    // Public method to start the game
    public void PlayGame()
    {
        // Loads the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Public method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
