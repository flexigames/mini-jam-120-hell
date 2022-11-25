using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float waveInterval;

    int waveNumber = 1;

    float remainingWaveTime = 0f;

    float remainingSpawnTime = 0f;

    void Start()
    {
        remainingWaveTime = waveInterval;
    }

    void Update()
    {
        if (remainingSpawnTime <= 0f)
        {
            SpawnEnemy();
            remainingSpawnTime = getSpawnInterval();
        }

        if (remainingWaveTime <= 0f)
        {
            waveNumber++;
            remainingWaveTime = waveInterval;
        }

        remainingSpawnTime -= Time.deltaTime;
        remainingWaveTime -= Time.deltaTime;
    }

    float getSpawnInterval()
    {
        return 3f / waveNumber;
    }

    void SpawnEnemy()
    {
        var randomPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
