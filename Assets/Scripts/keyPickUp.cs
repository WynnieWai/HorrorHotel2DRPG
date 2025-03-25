using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPickUp : MonoBehaviour
{
    public AudioClip collectSound; // Optional sound for collection
       void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player has picked up the key
            GameManager.instance.hasKey = true;
                // Play sound if assigned
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

            Destroy(gameObject); // Destroy the keypickup object
        }
    }
}
