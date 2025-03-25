using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCanvasManager : MonoBehaviour
{
    public GameObject storyCanvas; // Reference to the Story Canvas GameObject

    private void Start()
    {
        // Ensure the canvas is active at the start of the first scene
        if (storyCanvas != null)
        {
            storyCanvas.SetActive(true);
        }
    }

    public void CloseStoryCanvas()
    {
        // Close the canvas when the button is clicked
        if (storyCanvas != null)
        {
            storyCanvas.SetActive(false);
        }
    }
}
