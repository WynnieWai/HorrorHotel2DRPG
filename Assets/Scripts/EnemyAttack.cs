using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 1; // Damage dealt per hit
    public Vector2 knockbackForce = new Vector2(5, 5); // Knockback force applied to enemies
    // Start is called before the first frame update 
    // This method handles the collision with the player in the hitboxes
    private void OnTriggerEnter2D(Collider2D collision)
    {
           if (collision.CompareTag("Player")) // Check if the object is a player
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            // Apply damage to the player
            player.TakeDamage(attackDamage);

            // Apply knockback to the player (now with a duration argument)
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            float knockbackDuration = 0.5f; // Set the duration of the knockback
            player.ApplyKnockback(knockbackDirection * knockbackForce.magnitude, knockbackDuration);
        }
    }
    }
}
