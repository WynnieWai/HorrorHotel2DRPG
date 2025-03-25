using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
     public MovableObject movableObject; // Assign this in the Inspector

    void Start()
    {
        if (GameManager.instance != null)
        {
            // Load the saved position and canMove state
            movableObject.transform.position = GameManager.instance.movableObjectPosition;
            movableObject.canMove = GameManager.instance.movableObjectCanMove;
        }
    }
}
