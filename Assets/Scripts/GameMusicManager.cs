using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour
{  
        private static GameMusicManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource attached to this object
        }
        else
        {
            Destroy(gameObject);
        }

        // Listen to scene changes to stop/start music appropriately
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If the loaded scene is the main menu, stop the music
        if (scene.name == "Main Menu")
        {
            StopMusic();
        }
        else
        {
            // If it's a game scene (not main menu), play the music again
            PlayMusic();
        }
    }

    private void StopMusic()
    {
        // Stop the music when the main menu scene is loaded
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void PlayMusic()
    {
        // Start the music if it's not already playing
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        // Remove the listener when this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
