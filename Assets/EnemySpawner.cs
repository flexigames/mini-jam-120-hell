using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnEverySeconds;

    float remainingSpawnTime = 0f;

    void Update()
    {
        if (remainingSpawnTime <= 0f)
        {
            SpawnEnemy();
            remainingSpawnTime = spawnEverySeconds;
        }

        remainingSpawnTime -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        var randomPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
