using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   
    public Vector2 knockbackForce = new Vector2(5, 5); // Knockback force applied to enemies

 public GameManager gameManager; // Reference to GameManager

    private void Start()
    {
        // Assign GameManager reference
        if (GameManager.instance != null)
        {
            gameManager = GameManager.instance;
        }
        else
        {
            Debug.LogError("GameManager is not initialized.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (!PlayerMovement.isPlayerAlive)
    {
        return;
    }

    if (collision.CompareTag("enemy")) // Check if the object is an enemy
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null&& gameManager != null)
        {
            // Deal damage to the enemy
            enemy.TakeDamage(gameManager.attackPower);

            // Apply knockback to the enemy
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            enemy.ApplyKnockback(knockbackDirection * knockbackForce.magnitude);
        }
    }
      if (collision.CompareTag("enemy1")) // Check if the object is an enemy
    {
        Enemy enemy1 = collision.GetComponent<Enemy>();
        if (enemy1 != null&& gameManager != null)
        {
            // Deal damage to the enemy
            enemy1.TakeDamage(gameManager.attackPower);

            // Apply knockback to the enemy
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            enemy1.ApplyKnockback(knockbackDirection * knockbackForce.magnitude);
        }
    }

    if (collision.CompareTag("BreakableObject"))
        {
            BreakableObject breakable = collision.GetComponent<BreakableObject>();
            if (breakable != null)
            {
                breakable.TakeDamage(gameManager.attackPower);
            }
        }

    }
}
