using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailedStatsController : MonoBehaviour
{
       [Header("Detailed Stats Components")]
    public GameObject detailedStatsWindow;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public Button toggleButton;
    public Button closeButton; // Add reference for close button

    private GameManager gameManager;
    private bool isWindowOpen = false;

    private void Start()
    {
        gameManager = GameManager.instance;
        detailedStatsWindow.SetActive(false);
        toggleButton.onClick.AddListener(ToggleDetailedStats);
        
        // Add listener for close button
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseDetailedStats);
        }
        else
        {
            Debug.LogWarning("Close button reference is missing!");
        }
    }

    public void ToggleDetailedStats()
    {
        isWindowOpen = !isWindowOpen;
        detailedStatsWindow.SetActive(isWindowOpen);
        if (isWindowOpen)
        {
            UpdateDetailedStats();
        }
    }

    // New method to handle closing the window
    public void CloseDetailedStats()
    {
        isWindowOpen = false;
        detailedStatsWindow.SetActive(false);
    }

    private void UpdateDetailedStats()
    {
        healthText.text = $"Health: {gameManager.currentHealth}/{gameManager.maxHealth}";
        attackText.text = $"Attack: {gameManager.attackPower}";
        defenseText.text = $"Defense: {gameManager.defensePower}";
    }

    // Clean up listeners when the object is destroyed
    private void OnDestroy()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.RemoveListener(ToggleDetailedStats);
        }
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(CloseDetailedStats);
        }
    }
}
