using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public float knockbackResistance = 1f; // Factor to reduce knockback

    public int xpValue = 10; 
    public bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize health or other variables if needed
            if (GameManager.instance.defeatedEnemies.Contains(enemyName))
    {
        enabled = false;
        gameObject.SetActive(false);
    }
    else
    {
         health = 100; // Example initial health
    }
       
    }

    // Update is called once per frame
    void Update()
    {
        // Implement logic for enemy behavior (movement, attack, etc.)
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
         if (isInvincible) return;
        health -= damage;
        if (health <= 0)
        {
            Die(); // Implement the death logic
        }
        isInvincible = true;
        Invoke("ResetInvincibility", 1f);
    }

        private void ResetInvincibility()
    {
        isInvincible = false;
    }

    // Method to apply knockback
    public void ApplyKnockback(Vector2 knockback)
    {
        // Apply knockback while considering resistance
        Vector2 finalKnockback = knockback / knockbackResistance;
        Rigidbody2D rb = GetComponent<Rigidbody2D>(); // Assuming enemy has a Rigidbody2D

        if (rb != null)
        {
            rb.AddForce(finalKnockback, ForceMode2D.Impulse);
        }
    }

    // Method to handle enemy death (you can customize this)
    public virtual void Die()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.AddXP(xpValue);
        }
        Destroy(gameObject); 
    }
}
