using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // Required for TextMeshPro
 
public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private Queue<string> dialogueQueue = new Queue<string>();
    private bool isDialogueActive = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        dialogueBox.SetActive(false);
    }

    public void StartDialogue(string[] lines)
    {
        if (isDialogueActive) return;

        dialogueQueue.Clear();
        foreach (string line in lines)
        {
            dialogueQueue.Enqueue(line);
        }

        dialogueBox.SetActive(true);
        isDialogueActive = true;
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogueQueue.Dequeue();
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        isDialogueActive = false;
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }

}