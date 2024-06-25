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

    void Update()
    {
        // Remove any destroyed enemies from the list
        activeEnemies.RemoveAll(enemy => enemy == null);

        // Check if it's time to spawn a new enemy and if the max number of enemies is not exceeded
        if (Time.time > nextSpawn && activeEnemies.Count < maxEnemies)
        {
            nextSpawn = Time.time + spawnRate;

            // Calculate a random spawn position at the top of the viewport
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), 6f);

            // Instantiate a new enemy at the spawn position
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(newEnemy);

            // Debug log for spawning new enemy
            Debug.Log("Spawned Enemy at: " + Time.time + " Position: " + spawnPosition);
        }
    }
}
