using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D theRB;
    private Animator myAnim;
    //public int maxHealth = 5;
    //private int currentHealth;

    public float moveSpeed = 5f;
    public static PlayerMovement instance;

    private Vector2 movementInput;
    private Vector2 lastMove;
    private PlayerState currentState;

    public static bool isPlayerAlive = true;
    public AudioClip attackSound;
    public AudioClip walkSound;  // Drag the walk sound clip here in the Inspector
    private AudioSource audioSource;
    public AudioClip gameOverSound;
    private bool isWalking = false;  // To track if the player is currently walking


    // XP and leveling system




    void Start()
    {
        currentState = PlayerState.walk;
        theRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
         audioSource = GetComponent<AudioSource>();
       


        // Reset static variables
        isPlayerAlive = true;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
 

        StartCoroutine(InitializePlayerPosition());
        
    }

    private IEnumerator InitializePlayerPosition()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Vector2 spawnPoint = GameManager.instance.GetSpawnPoint(sceneName);

        FadeManager.instance.SetOverlayAlpha(1f);

        if (spawnPoint != Vector2.zero)
        {
            transform.position = spawnPoint;
        }
       

        yield return FadeManager.instance.FadeIn();
    }

    // public void EarnXP(int amount)
    // {
    //   currentXP += amount;
    // if (GameManager.instance != null)
    // {
    //     GameManager.instance.currentXP = currentXP; // Sync XP to GameManager
    //     GameManager.instance.SavePlayerStats();     // Save stats
    //     Debug.Log($"EarnXP: Synced XP with GameManager. Current XP: {GameManager.instance.currentXP}");
    // }
    // Debug.Log($"Gained {amount} XP. Total XP: {currentXP}");

    // if (currentXP >= requiredXPForNextLevel)
    // {
    //     LevelUp();
    // }
    // }

    // private void LevelUp()
    // {
    //    Debug.Log($"Level Up! Current Level: {currentLevel}");

    // // Check how much XP remains after leveling up
    // currentXP -= requiredXPForNextLevel;

    // // Update stats for the new level
    // currentLevel++;
    // maxHealth += 10; // Example: Increase max health
    // attackPower += 2; // Example: Increase attack power
    // defensePower += 1; // Example: Increase defense

    // // Adjust XP requirement for the next level
    // requiredXPForNextLevel = currentLevel * 100;

    // // Sync with GameManager
    // if (GameManager.instance != null)
    // {
    //     GameManager.instance.currentLevel = currentLevel;
    //     GameManager.instance.SavePlayerStats(); // Save stats
    //     Debug.Log($"LevelUp: Synced Level with GameManager. Current Level: {GameManager.instance.currentLevel}");
    // }

    // Debug.Log($"New Level: {currentLevel}, XP for next level: {requiredXPForNextLevel}, Remaining XP: {currentXP}");
    // }

    public void TakeDamage(int damage)
    {
        if (currentState == PlayerState.stagger)
        {
            return; // Prevent taking damage if already in stagger state
        }

        int reducedDamage = Mathf.Max(damage - GameManager.instance.defensePower, 1);
        GameManager.instance.currentHealth -= reducedDamage;
        Debug.Log($"Player took {reducedDamage} damage (reduced by {GameManager.instance.defensePower}). Current health: {GameManager.instance.currentHealth}");
        if (GameManager.instance.currentHealth <= 0)
        {
            Debug.Log("Player has died! Triggering death logic.");
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayerAlive) return;

        theRB.velocity = Vector2.zero;

        myAnim.SetFloat("deathX", lastMove.x);
        myAnim.SetFloat("deathY", lastMove.y);

        myAnim.SetTrigger("Die");
        myAnim.SetBool("attacking", false);
        currentState = PlayerState.stagger;
        isPlayerAlive = false;

        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(0.5f);
    if (gameOverSound != null)
    {
        AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
    }
        GameOver.instance.ShowGameOverScreen();
    }

    void FixedUpdate()
    {
        if (currentState == PlayerState.walk)
        {
            HandleMovement();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        if (!context.performed)
        {
            movementInput = Vector2.zero;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
    if (context.performed && currentState == PlayerState.walk && GameManager.instance.hasSword)
            {
                StartCoroutine(AttackCo());
            }
    }

    private void HandleMovement()
    {
        if (currentState == PlayerState.walk)
        {
            theRB.velocity = movementInput * moveSpeed;

            myAnim.SetFloat("moveX", movementInput.x);
            myAnim.SetFloat("moveY", movementInput.y);

            bool isMoving = movementInput != Vector2.zero;

            if (isMoving)
            {
                lastMove = movementInput;
                myAnim.SetBool("moving", true);
                         // Play the walking sound if it's not already playing
                if (!isWalking && walkSound != null)
                {
                    audioSource.loop = true; // Loop the walking sound
                    audioSource.clip = walkSound;
                    audioSource.Play();
                    isWalking = true;
                }
            }
            else
            {
                myAnim.SetBool("moving", false);
                myAnim.SetFloat("lastMoveX", lastMove.x);
                myAnim.SetFloat("lastMoveY", lastMove.y);
                         // Stop the walking sound if player stops moving
                if (isWalking)
                {
                    audioSource.Stop();
                    isWalking = false;
                }
            }
        }
    }

    private IEnumerator AttackCo()
    {
        myAnim.SetFloat("lastMoveX", lastMove.x);
        myAnim.SetFloat("lastMoveY", lastMove.y);

        myAnim.SetBool("attacking", true);
        currentState = PlayerState.attack;
        if (attackSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
        yield return null;
        myAnim.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.walk;
    }

    public void ApplyKnockback(Vector2 force, float duration)
    {
        StartCoroutine(KnockbackCoroutine(force, duration));
    }

    private IEnumerator KnockbackCoroutine(Vector2 force, float duration)
    {
        currentState = PlayerState.stagger;
        theRB.velocity = force;

        yield return new WaitForSeconds(duration);

        theRB.velocity = Vector2.zero;
        currentState = PlayerState.walk;
    }
    public IEnumerator ApplySpeedBoost(float speedIncreaseAmount, float duration)
{
    moveSpeed += speedIncreaseAmount;
    Debug.Log($"Speed increased by {speedIncreaseAmount}. New speed: {moveSpeed}");

    yield return new WaitForSeconds(duration);

    moveSpeed -= speedIncreaseAmount;
    Debug.Log($"Speed boost ended. Speed reverted to {moveSpeed}");
}
}
