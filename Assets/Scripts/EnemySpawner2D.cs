using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2D : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    private float nextSpawn = 0f;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), 6f);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
