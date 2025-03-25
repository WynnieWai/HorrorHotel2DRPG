using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoBackMainMenu : MonoBehaviour
{
    public string mainMenuSceneName = "Main Menu"; // Set the name of your main menu scene in the Inspector

    public void BackToMainMenu()
    {
        Debug.Log($"Returning to main menu: {mainMenuSceneName}");
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
