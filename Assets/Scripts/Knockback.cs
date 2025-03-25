using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust; // Force of knockback
    public float knockTime; // Duration of knockback

    private void OnTriggerEnter2D(Collider2D other)
    {
           if (other.CompareTag("Player"))
    {
        Rigidbody2D playerRB = other.GetComponent<Rigidbody2D>();
        PlayerMovement playerScript = other.GetComponent<PlayerMovement>();

        if (playerRB != null && playerScript != null)
        {
            // Calculate the knockback direction
            Vector2 difference = (playerRB.transform.position - transform.position).normalized;

            // Debug log for the difference
            Debug.Log($"Raw knockback direction: {difference}");

            // Ensure a minimum knockback force for horizontal direction
            if (Mathf.Abs(difference.x) < 0.01f) // Adjust threshold as needed
            {
                difference.x = difference.x < 0 ? -0.05f : 0.05f; // Ensure minimum horizontal force
            }

            // Apply thrust
            Vector2 force = difference * thrust;

            // Debug log for the final force
            Debug.Log($"Knockback force applied: {force}");

            // Apply knockback
            playerScript.ApplyKnockback(force, knockTime);
        }
    }

        if (other.CompareTag("enemy"))
        {
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();
            Enemy1 enemyScript = other.GetComponent<Enemy1>();

            if (enemyRigidbody != null && enemyScript != null)
            {
                enemyScript.currentState = EnemyState.stagger;
                Vector2 difference = (enemyRigidbody.transform.position - transform.position).normalized * thrust;
                enemyScript.ApplyKnockback(difference, knockTime);
            }
        }
            if (other.CompareTag("enemy1"))
        {
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();
            Enemy2 enemyScript = other.GetComponent<Enemy2>();

            if (enemyRigidbody != null && enemyScript != null)
            {
                enemyScript.currentState = EnemyState.stagger;
                Vector2 difference = (enemyRigidbody.transform.position - transform.position).normalized * thrust;
                enemyScript.ApplyKnockback(difference, knockTime);
            }
        }
    }
}