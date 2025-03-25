using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float speedIncreaseAmount = 2f;
    public float duration = 5f;
    
    public AudioClip speedUpSound; // Optional sound for collection


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
                // Play sound if assigned
            if (speedUpSound != null)
            {
                AudioSource.PlayClipAtPoint(speedUpSound, transform.position);
            }
            if (player != null)
            {
                player.StartCoroutine(player.ApplySpeedBoost(speedIncreaseAmount, duration));
                Destroy(gameObject);
            }
        }
    }
}
