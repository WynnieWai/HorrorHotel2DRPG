using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For UI elements
using TMPro;

public class CollectibleItem : MonoBehaviour
{
  public string itemID; // Unique identifier for this collectible
    public string itemName = "Collectible"; // Name of the item
    public AudioClip collectSound; // Optional sound for collection
    private bool isPlayerNearby = false;
    public int healthAmount = 1;
   public TMP_Text collectibleMessage; // Reference to the UI Text element

    private void Start()
    {
        // Check if the item has already been collected
        if (GameManager.instance != null && GameManager.instance.collectedItems.Contains(itemID))
        {
            Destroy(gameObject); // Item has already been collected, so destroy it
        }
               // Hide the message initially
        if (collectibleMessage != null)
        {
            collectibleMessage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.C))
        {
            Collect();
             // Reward player with XP
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log($"Player is near {itemName}!");
                        // Show the message
            if (collectibleMessage != null)
            {
                collectibleMessage.text = $"Hint: Evidence is near around you";
                collectibleMessage.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log($"Player left the area of {itemName}.");
                     // Hide the message
            if (collectibleMessage != null)
            {
                collectibleMessage.gameObject.SetActive(false);
            }
        }
    }

    private void Collect()
    {
            // Mark item as collected
    if (GameManager.instance != null)
    {
        GameManager.instance.collectedItems.Add(itemID);
        GameManager.instance.SaveCollectedItems(); // Save the state
    }

    // Play sound if assigned
    if (collectSound != null)
    {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
    }

    // Give XP to player
    if (GameManager.instance != null)
    {
        GameManager.instance.AddXP(20);
    }

    // Increase health by healthAmount without exceeding maxHealth
    if (GameManager.instance != null)
    {
        int newHealth = GameManager.instance.currentHealth + healthAmount;
        GameManager.instance.currentHealth = Mathf.Min(newHealth, GameManager.instance.maxHealth);
        Debug.Log($"Health increased by {healthAmount}.");
        GameManager.instance.SavePlayerStats(); // Save the updated health
        // Update UI display
        // if (GameManager.instance != null)
        // {
        //     GameManager.instance.UpdateHealthDisplay();
        // }
    }
            // Hide the message
        if (collectibleMessage != null)
        {
            collectibleMessage.gameObject.SetActive(false);
        }


    // Destroy the collectible object
    Destroy(gameObject);
    Debug.Log($"Collected {itemName}!");
    }
}
