using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{
   [Header("Health Bar Components")]
    public Image healthBarBackground;
    public Image healthBarForeground;
    public TextMeshProUGUI healthText;
    
    // [Header("Health Bar Settings")]
    // public Color lowHealthColor = new Color(0.8f, 0.2f, 0.2f);
    // public Color fullHealthColor = new Color(1f, 0.2f, 0.2f);
    
    private GameManager gameManager;
    private float lastMaxHealth;
    private float lastCurrentHealth;

    private void Start()
    {
        gameManager = GameManager.instance;
        lastMaxHealth = gameManager.maxHealth;
        lastCurrentHealth = gameManager.currentHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (lastMaxHealth != gameManager.maxHealth || lastCurrentHealth != gameManager.currentHealth)
        {
            UpdateHealthBar();
            lastMaxHealth = gameManager.maxHealth;
            lastCurrentHealth = gameManager.currentHealth;
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercent = (float)gameManager.currentHealth / gameManager.maxHealth;
        healthBarForeground.fillAmount = healthPercent;
       // healthBarForeground.color = Color.Lerp(lowHealthColor, fullHealthColor, healthPercent);
        healthText.text = $"{gameManager.currentHealth}/{gameManager.maxHealth}";
    }
}
