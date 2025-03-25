using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    
public AudioClip collectSound; // Optional sound for collection


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the sword object
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.hasSword = true;

    // Play sound if assigned
    if (collectSound != null)
    {
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
    }

            // Save the player's updated stats
            GameManager.instance.SavePlayerStats();

            // Destroy the sword object
            Destroy(gameObject);
        }
    }
}
