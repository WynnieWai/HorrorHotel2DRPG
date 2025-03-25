using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectibleManager : MonoBehaviour
{
    public Tilemap collectibleTilemap; // Assign the Tilemap in the Inspector
    public AudioClip collectSound; // Optional: assign a sound for collection
    private AudioSource audioSource;
    private Transform playerTransform; // Reference to the player's transform
    private int collectedCount = 0;

    void Start()
    {
        // Find the player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        if (collectSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = collectSound;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TryCollect();
        }
    }

    void TryCollect()
    {
        if (playerTransform == null) return;

        // Get the player's current tile position
        Vector3 playerPos = playerTransform.position;
        Vector3Int cellPosition = collectibleTilemap.WorldToCell(playerPos);

        if (collectibleTilemap.HasTile(cellPosition))
        {
            // Collect the tile
            collectibleTilemap.SetTile(cellPosition, null);
            collectedCount++;

            // Play sound
            if (audioSource != null)
                audioSource.Play();

            Debug.Log($"Collected Items: {collectedCount}");
        }
        else
        {
            Debug.Log("No collectible item nearby!");
        }
    }
}
