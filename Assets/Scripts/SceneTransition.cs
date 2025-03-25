using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetScene; // Name of the scene to load
    public Vector2 spawnPosition; // Player's spawn position in the target scene
    public int requiredXPforTrans = 100;
    public GameObject hintPanel; // Reference to the Hint UI Panel
    public float hintDisplayDuration = 2f; // How long to show the hint

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag("Player"))
    {
        Debug.Log("Player entered the scene transition area.");

        // Check if player XP and Level requirements are met
        if (GameManager.instance != null)
        {
            Debug.Log($"Checking requirements: Current XP = {GameManager.instance.currentXP}, Required XP = {requiredXPforTrans*GameManager.instance.currentLevel}, Current Level = {GameManager.instance.currentLevel}");
          
                StartCoroutine(TransitionToScene());
            
      
        }
        else
        {
            Debug.LogError("GameManager instance is null!");
        }
    }
    }

    private IEnumerator TransitionToScene()
    {
        
        // Start fade out
        if (FadeManager.instance != null)
        {
            yield return FadeManager.instance.FadeOut();
        }

        // Save the spawn position for the target scene
        GameManager.instance.SetSpawnPoint(spawnPosition);

        // Load the target scene
        SceneManager.LoadScene(targetScene);

        // Wait for the scene to finish loading
        yield return null;

        // Start fade in
        if (FadeManager.instance != null)
        {
            yield return FadeManager.instance.FadeIn();
        }
    }

    private void ShowHint()
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(true); // Show the hint panel
            StartCoroutine(HideHintAfterDelay());
        }
        else
        {
            Debug.LogWarning("Hint Panel not assigned in the Inspector!");
        }
    }

    private IEnumerator HideHintAfterDelay()
    {
        yield return new WaitForSeconds(hintDisplayDuration);
        hintPanel.SetActive(false); // Hide the hint panel
    }
}

