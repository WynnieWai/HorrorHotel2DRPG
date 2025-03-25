using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintBoxManager : MonoBehaviour
{
   public static HintBoxManager instance;

    public GameObject hintBox;
    public TextMeshProUGUI hintBoxText;

    private Queue<string> hintBoxQueue = new Queue<string>();
    private bool isHintBoxActive = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        hintBox.SetActive(false);
    }

    public void StartHintBox(string[] lines)
    {
        if (isHintBoxActive) return;

        hintBoxQueue.Clear();
        foreach (string line in lines)
        {
            hintBoxQueue.Enqueue(line);
        }

        hintBox.SetActive(true);
        isHintBoxActive = true;
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (hintBoxQueue.Count == 0)
        {
            EndHintBox();
            return;
        }

        hintBoxText.text = hintBoxQueue.Dequeue();
    }

    public void EndHintBox()
    {
        hintBox.SetActive(false);
        isHintBoxActive = false;
    }

    public bool IsHintBoxActive()
    {
        return isHintBoxActive;
    }

    void Update()
    {
        if (isHintBoxActive && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }
}
