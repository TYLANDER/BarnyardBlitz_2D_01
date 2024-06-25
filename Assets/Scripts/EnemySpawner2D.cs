using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner2D : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab for the enemy
    public float spawnRate = 2f; // Rate at which enemies spawn
    private float nextSpawn = 0f; // Time until the next spawn
    public int maxEnemies = 5; // Maximum number of enemies on screen at once
    private List<GameObject> activeEnemies = new List<GameObject>(); // List to keep track of active enemies

    public GameObject background; // Reference to the background object
    private float minX, maxX, minY, maxY; // Bounds of the background

    void Start()
    {
        CalculateBackgroundBounds();
    }

    void Update()
    {
        // Remove any destroyed enemies from the list
        activeEnemies.RemoveAll(enemy => enemy == null);

        // Check if it's time to spawn a new enemy and if the max number of enemies is not exceeded
        if (Time.time > nextSpawn && activeEnemies.Count < maxEnemies)
        {
            nextSpawn = Time.time + spawnRate;

            // Calculate a random spawn position just above the top of the background bounds
            Vector2 spawnPosition = new Vector2(Random.Range(minX - 3f, maxX + 3f), maxY + 5f); // Adding 1f to ensure they spawn out of view

            // Instantiate a new enemy at the spawn position
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(newEnemy);

            // Debug log for spawning new enemy
            Debug.Log("Spawned Enemy at: " + Time.time + " Position: " + spawnPosition);
        }
    }

    void CalculateBackgroundBounds()
    {
        SpriteRenderer renderer = background.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            minX = renderer.bounds.min.x;
            maxX = renderer.bounds.max.x;
            minY = renderer.bounds.min.y;
            maxY = renderer.bounds.max.y;
        }
        else
        {
            Debug.LogError("Background does not have a SpriteRenderer component.");
        }
    }
}
