using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBarController : MonoBehaviour
{
     [Header("XP Bar Components")]
    public Image xpBarBackground;
    public Image xpBarForeground;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    
    // [Header("XP Bar Settings")]
    // public Color lowXPColor = new Color(0.2f, 0.4f, 0.8f);
    // public Color fullXPColor = new Color(0.4f, 0.6f, 1f);

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        UpdateXPBar();
    }

    private void Update()
    {
        UpdateXPBar();
    }

    private void UpdateXPBar()
    {
        float xpPercent = (float)gameManager.currentXP / gameManager.requiredXPForNextLevel;
        xpBarForeground.fillAmount = xpPercent;
        //xpBarForeground.color = Color.Lerp(lowXPColor, fullXPColor, xpPercent);
        
        levelText.text = $"Level {gameManager.currentLevel}";
        xpText.text = $"{gameManager.currentXP}/{gameManager.requiredXPForNextLevel}";
    }
}
