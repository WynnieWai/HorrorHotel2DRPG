using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public static GameManager instance;

    private Vector2 spawnPoint;
    private Dictionary<string, Vector2> initialSpawnPoints;
    public int currentXP;
    public int currentLevel;
    public int requiredXPForNextLevel = 100;

    public int maxHealth = 5;
    public int attackPower = 1;
    public int defensePower = 5;
    public int currentHealth;

    public bool hasSword = false;

    public Vector3 movableObjectPosition;
    public bool movableObjectCanMove = true;
    public bool hasKey = false;
    public bool hasKeyDropped = false;

    public HashSet<string> collectedItems = new HashSet<string>();
    public HashSet<string> defeatedEnemies = new HashSet<string>();
    public AudioClip levelUpSound;  // Add this line
    private AudioSource audioSource; // Add this line

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            EnsureFadeManagerExists();    // Ensure FadeManager is initialized
            InitializeSpawnPoints();      // Setup spawn points
            LoadCollectedItems();         // Load collected items from storage
            LoadPlayerStats();
            LoadDefeatedEnemies();
             audioSource = GetComponent<AudioSource>();  // Initialize the AudioSource
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Debug.Log("GameManager initialized in scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void InitializeSpawnPoints()
    {
        initialSpawnPoints = new Dictionary<string, Vector2>
        {
            { "The Lobby", new Vector2(1, -19) },
            { "The Hotel Corridor", new Vector2(-22, -1) },
            //{ "The Guest Room", new Vector2(-17, -17) },
            { "The Bar and Louge", new Vector2(-36, 5) },
            { "The Secret Room", new Vector2(-4, -15) },
        };
    }

    public void SetSpawnPoint(Vector2 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public Vector2 GetSpawnPoint(string sceneName)
    {
        return spawnPoint != Vector2.zero ? spawnPoint : 
               initialSpawnPoints.ContainsKey(sceneName) ? initialSpawnPoints[sceneName] : 
               Vector2.zero;
    }

    private void EnsureFadeManagerExists()
    {
        if (FadeManager.instance == null)
        {
            GameObject fadeManagerPrefab = Resources.Load<GameObject>("Fade Manager");
            if (fadeManagerPrefab != null)
            {
                Instantiate(fadeManagerPrefab);
            }
            else
            {
                Debug.LogError("FadeManager prefab not found in Resources folder!");
            }
        }
    }
    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log($"Gained {amount} XP. Total XP: {currentXP}");
        while (currentXP >= requiredXPForNextLevel)
        {
            LevelUp();
        }
        SavePlayerStats();
    }
  public void LevelUp()
    {
        currentXP -= requiredXPForNextLevel;
        currentLevel++;
        requiredXPForNextLevel = currentLevel * 100;
        maxHealth += 5; // Increase max health
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        //currentHealth = maxHealth;
        attackPower += 10; // Increase attack power
        defensePower += 1; // Increase defense
             // Play level-up sound
        if (levelUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(levelUpSound);  // Play the level-up sound effect
        }
        Debug.Log($"Leveled up to level {currentLevel}! " +
                  $"XP required for next level: {requiredXPForNextLevel}, " +
                  $"Max Health: {maxHealth}, Attack: {attackPower}, Defense: {defensePower}");
        SavePlayerStats();
    }

    public void SavePlayerStats()
    {
         PlayerPrefs.SetInt("CurrentXP", currentXP);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.SetInt("CurrentHealth", currentHealth);
        PlayerPrefs.SetInt("AttackPower", attackPower);
        PlayerPrefs.SetInt("DefensePower", defensePower);
        PlayerPrefs.SetInt("HasSword", hasSword ? 1 : 0);
        PlayerPrefs.SetFloat("MovableObjectPosX", movableObjectPosition.x);
        PlayerPrefs.SetFloat("MovableObjectPosY", movableObjectPosition.y);
        PlayerPrefs.SetFloat("MovableObjectPosZ", movableObjectPosition.z);
        PlayerPrefs.SetInt("MovableObjectCanMove", movableObjectCanMove ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadPlayerStats()
    {
               if (PlayerPrefs.HasKey("CurrentXP") && PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentXP = PlayerPrefs.GetInt("CurrentXP");
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            maxHealth = PlayerPrefs.GetInt("MaxHealth", 5);
            currentHealth = PlayerPrefs.GetInt("CurrentHealth", maxHealth);
            attackPower = PlayerPrefs.GetInt("AttackPower", 10);
            defensePower = PlayerPrefs.GetInt("DefensePower", 5);
            requiredXPForNextLevel = currentLevel * 100;
             hasSword = PlayerPrefs.GetInt("HasSword", 0) == 1;
              movableObjectPosition = new Vector3(PlayerPrefs.GetFloat("MovableObjectPosX", 0f),
                                            PlayerPrefs.GetFloat("MovableObjectPosY", 0f),
                                            PlayerPrefs.GetFloat("MovableObjectPosZ", 0f));
        movableObjectCanMove = PlayerPrefs.GetInt("MovableObjectCanMove", 1) == 1;
            Debug.Log("Player stats loaded.");
        }
        else
        {
            currentXP = 0;
            currentLevel = 1;
            maxHealth = 5;
            currentHealth = maxHealth; 
            attackPower = 1;
            defensePower = 5;
            requiredXPForNextLevel = 100;
            Debug.Log("Initializing default values.");
        }
        
    }

    // Save collected items to PlayerPrefs
    public void SaveCollectedItems()
    {
        PlayerPrefs.SetString("CollectedItems", string.Join(",", collectedItems));
        PlayerPrefs.Save();
        Debug.Log("Collected items saved.");
    }

    // Load collected items from PlayerPrefs
    public void LoadCollectedItems()
    {
        if (PlayerPrefs.HasKey("CollectedItems"))
        {
            string data = PlayerPrefs.GetString("CollectedItems");
            collectedItems = new HashSet<string>(data.Split(','));
            Debug.Log("Collected items loaded.");
        }
        else
        {
            Debug.Log("No collected items data found.");
        }
    }

      public void DefeatEnemy(string enemyName)
    {
        defeatedEnemies.Add(enemyName);
        SaveDefeatedEnemies();
    }

        // Save defeated enemies to PlayerPrefs
    public void SaveDefeatedEnemies()
    {
        List<string> defeatedList = new List<string>(defeatedEnemies);
        string defeatedData = string.Join(",", defeatedList);
        PlayerPrefs.SetString("DefeatedEnemies", defeatedData);
        PlayerPrefs.Save();
    }

    // Load defeated enemies from PlayerPrefs
    public void LoadDefeatedEnemies()
    {
        if (PlayerPrefs.HasKey("DefeatedEnemies"))
        {
            string data = PlayerPrefs.GetString("DefeatedEnemies");
            defeatedEnemies = new HashSet<string>(data.Split(','));
            Debug.Log("Defeated enemies loaded.");
        }
        else
        {
            Debug.Log("No defeated enemies data found.");
        }
    }

    // Clear all collected items (use this to reset progress for a new game)
public void ResetPlayerProgress()
{
    currentXP = 0;
    currentLevel = 1;
    requiredXPForNextLevel = 100;
    maxHealth = 5;
    currentHealth = maxHealth;
    attackPower = 1;
    defensePower = 5;
    hasSword = false;
    hasKey = false;
    hasKeyDropped = false;
    movableObjectPosition = Vector3.zero;
    movableObjectCanMove = true;
    collectedItems.Clear();
    defeatedEnemies.Clear();
    PlayerPrefs.DeleteKey("CurrentXP");
    PlayerPrefs.DeleteKey("CurrentLevel");
    PlayerPrefs.DeleteKey("CollectedItems");
    PlayerPrefs.DeleteKey("MaxHealth");
    PlayerPrefs.DeleteKey("CurrentHealth");
    PlayerPrefs.DeleteKey("AttackPower");
    PlayerPrefs.DeleteKey("DefensePower");

    PlayerPrefs.Save();
    // Optionally reset spawnPoint to initial position
    spawnPoint = Vector2.zero; // or set to initial spawn position
    Debug.Log("Player progress reset.");
}
    
}
