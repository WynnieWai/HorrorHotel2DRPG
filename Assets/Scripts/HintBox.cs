using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HintBox : MonoBehaviour
{
    [TextArea]
    public string[] hintBoxLines;

    public GameObject hintBoxSign; // Reference to the blank dialogue box
    private bool isPlayerInRange = false; // Tracks if the player is within range
    private bool hasInteracted = false;  // Prevents repeated interactions

    void Start()
    {
        if (hintBoxSign != null)
        {
            hintBoxSign.SetActive(false); // Ensure the sign is hidden initially
        }
    }

    void Update()
    {
        // Check for interaction input only if player is in range, dialogue is inactive, and hasn't interacted yet
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !HintBoxManager.instance.IsHintBoxActive() && !hasInteracted)
        {
            HintBoxManager.instance.StartHintBox(hintBoxLines); // Start the dialogue
             GameManager.instance.AddXP(20);
            hasInteracted = true; // Mark as interacted
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Mark the player as in range
            if (hintBoxSign != null)
            {
                hintBoxSign.SetActive(true); // Show the blank dialogue box
            }
            Debug.Log("Player entered NPC range. Press E to interact.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Mark the player as out of range
            hasInteracted = false;   // Reset interaction flag
            if (hintBoxSign != null)
            {
                hintBoxSign.SetActive(false); // Hide the blank dialogue box
            }
            Debug.Log("Player left NPC range.");
        }
    }
}
