using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovableObject : MonoBehaviour
{
     public bool canMove = true;
    public float pushForce = 5f;

    private Rigidbody2D rb;
    private bool isBeingPushed = false;
    public AudioClip moveSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("MovableObject requires a Rigidbody2D component.");
        }
    }

    void FixedUpdate()
    {
        if (!isBeingPushed && rb.velocity.magnitude > 0.01f)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canMove && collision.gameObject.CompareTag("Player"))
        {
            isBeingPushed = true;
                // Play sound if assigned
        if (moveSound != null)
        {
            AudioSource.PlayClipAtPoint(moveSound, transform.position);
        }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (canMove && collision.gameObject.CompareTag("Player"))
        {
            Vector2 pushDirection = collision.contacts[0].normal * -1;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Force);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBeingPushed = false;
        }
    }

    void OnDisable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.movableObjectPosition = transform.position;
            GameManager.instance.movableObjectCanMove = canMove;
            GameManager.instance.SavePlayerStats();
        }
    }
}
