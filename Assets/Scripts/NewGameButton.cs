using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewGameButton : MonoBehaviour
{
     // Name of the first scene in your game
    public string firstSceneName = "The Lobby";

    public void StartNewGame()
    {
Debug.Log("Start New Game button clicked!");

    if (GameManager.instance != null)
    {
        Debug.Log("Resetting collected items...");
        GameManager.instance.ResetPlayerProgress();
    }

    Debug.Log("Loading scene: " + firstSceneName);
    SceneManager.LoadScene(firstSceneName);
    }

        public void ExitGame()
    {
        Debug.Log("Exit Game button clicked!");
        Application.Quit();

        // For debugging in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
