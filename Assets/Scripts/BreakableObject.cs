using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public int healthObject = 5;
    public int xpReward = 10;
    //public int healthReward = 2;
    public AudioClip breakSound;

    public GameObject speedUpPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            TakeDamage(1); // Assuming player attack deals 1 damage per hit
        }
    }

    public void TakeDamage(int damage)
    {
        healthObject -= damage;
        if (healthObject <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
      // Increase player's XP and health
    GameManager.instance.AddXP(xpReward);
    //GameManager.instance.currentHealth += healthReward;
    GameManager.instance.SavePlayerStats();
    // Play sound if assigned
    if (breakSound != null)
    {
        AudioSource.PlayClipAtPoint(breakSound, transform.position);
    }

    // Instantiate the speed-up collectible
    if (speedUpPrefab != null)
    {
        Instantiate(speedUpPrefab, transform.position, Quaternion.identity);
    }

    // Destroy the breakable object
    Destroy(gameObject);
    }
}
