using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{
     private static MainMenuMusicManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Stop the music if the current scene is not the main menu
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Main Menu")
        {
            Destroy(gameObject);
        }
    }
}
