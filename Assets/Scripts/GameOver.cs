using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over UI Panel
    public string mainMenuSceneName = "Main Menu"; // Name of the main menu scene

    //public AudioClip gameOverSound; // Optional sound for collection
    public static GameOver instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowGameOverScreen()
    {
        gameOverUI.SetActive(true);
            // Play sound if assigned
    // if (gameOverSound != null)
    // {
    //     AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
    // }

        Time.timeScale = 0f; // Pause the game
    }

    // public void RetryGame()
    // {
    //     Time.timeScale = 1f; // Resume the game
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    // }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(mainMenuSceneName); // Load the main menu
    }
}
