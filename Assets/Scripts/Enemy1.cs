using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
  private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;
    private bool isKnockedBack = false;
    private bool isAttacking = false; // Flag to prevent multiple attacks

    public GameObject keyPrefab;
    public bool isKeyDropper = false;

    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
             // Reset enemy flags
        isKnockedBack = false;
        isAttacking = false;
    }

    void FixedUpdate()
    {
        if (!isKnockedBack && !isAttacking)
        {
            CheckDistance();
        }
    }

    void CheckDistance()
    {
        if (!PlayerMovement.isPlayerAlive)
    {
        // Stop movement and switch to idle animation
        UpdateAnimation(Vector2.zero);
        return;
    }

    float distance = Vector3.Distance(target.position, transform.position);

    if (distance <= attackRadius)
    {
        // Enter attack behavior
        if (currentState != EnemyState.attack)
        {
            StartCoroutine(AttackPlayer());
        }
    }
    else if (distance <= chaseRadius)
    {
        // Chase behavior
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        Vector2 direction = (target.position - transform.position).normalized;

        UpdateAnimation(direction);
        myRigidbody.MovePosition(temp);
        ChangeState(EnemyState.walk);
    }
    else
    {
        // Idle behavior
        UpdateAnimation(Vector2.zero); // Idle animation
    }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void UpdateAnimation(Vector2 direction)
    {
        anim.SetBool("IsMoving", direction != Vector2.zero);

        if (direction != Vector2.zero)
        {
            anim.SetFloat("moveX", direction.x);
            anim.SetFloat("moveY", direction.y);
        }
    }

    private IEnumerator AttackPlayer()
    {
            if (!PlayerMovement.isPlayerAlive)
    {
        yield break;
    }

    isAttacking = true;
    ChangeState(EnemyState.attack);

    // Calculate direction to the player
    Vector2 direction = (target.position - transform.position).normalized;

    // Determine attack direction and update animator parameters
    anim.SetFloat("attackX", direction.x);
    anim.SetFloat("attackY", direction.y);
    anim.SetTrigger("Attack"); // Trigger attack animation

    yield return new WaitForSeconds(0.5f); // Wait for the attack animation to complete

    isAttacking = false;

    ChangeState(EnemyState.idle);
    }

    public void ApplyKnockback(Vector2 force, float duration)
    {
        StartCoroutine(KnockbackCoroutine(force, duration));
    }

    private IEnumerator KnockbackCoroutine(Vector2 force, float duration)
    {
        isKnockedBack = true;
        myRigidbody.velocity = force;

        yield return new WaitForSeconds(duration);

        myRigidbody.velocity = Vector2.zero;
        isKnockedBack = false;

        if (currentState == EnemyState.stagger)
        {
            ChangeState(EnemyState.idle);
        }
    }

    public override void Die()
    {
        base.Die();
        if (GameManager.instance != null)
        {
            GameManager.instance.DefeatEnemy(enemyName);
        }
        if (isKeyDropper && !GameManager.instance.hasKeyDropped)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
            GameManager.instance.hasKeyDropped = true;
        }
    }
}
