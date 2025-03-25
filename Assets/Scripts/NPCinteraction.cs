using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCinteraction : MonoBehaviour
{
    [TextArea]
    public string[] dialogueLines;

    public GameObject dialogueSign; // Reference to the blank dialogue box
    public GameObject closeButton;
    private bool isPlayerInRange = false; // Tracks if the player is within range
    private bool hasInteracted = false;  // Prevents repeated interactions

    void Start()
    {
        if (dialogueSign != null)
        {
            dialogueSign.SetActive(false); // Ensure the sign is hidden initially
        }
                if (closeButton != null)
        {
            closeButton.SetActive(false); // Ensure the button is hidden initially
        }
    }

    void Update()
    {
        // Check for interaction input only if player is in range, dialogue is inactive, and hasn't interacted yet
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !DialogueManager.instance.IsDialogueActive() && !hasInteracted)
        {
            DialogueManager.instance.StartDialogue(dialogueLines); // Start the dialogue
                       // Give XP to player
                                   if (closeButton != null)
            {
                closeButton.SetActive(true); // Show the close button
            }

            GameManager.instance.AddXP(50);
            hasInteracted = true; // Mark as interacted

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Mark the player as in range
            if (dialogueSign != null)
            {
                dialogueSign.SetActive(true); // Show the blank dialogue box
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
            if (dialogueSign != null)
            {
                dialogueSign.SetActive(false); // Hide the blank dialogue box
            }
                      if (closeButton != null)
            {
                closeButton.SetActive(false); // Hide the close button
            }
            Debug.Log("Player left NPC range.");
        }
    }
        // Function to close the dialogue
    public void CloseDialogue()
    {
        DialogueManager.instance.EndDialogue(); // End the dialogue
        if (closeButton != null)
        {
            closeButton.SetActive(false); // Hide the close button
        }
        hasInteracted = false; // Reset interaction flag to allow re-interaction if needed
        Debug.Log("Dialogue closed.");
    }
}
