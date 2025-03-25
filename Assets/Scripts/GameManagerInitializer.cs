using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerInitializer : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameObject gameManagerPrefab = Resources.Load<GameObject>("Game Manager");
            if (gameManagerPrefab != null)
            {
                Instantiate(gameManagerPrefab);
            }
            else
            {
                Debug.LogError("GameManager prefab not found in Resources folder!");
            }
        }
    }
}
