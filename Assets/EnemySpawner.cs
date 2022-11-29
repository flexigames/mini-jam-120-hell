using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public float waveInterval;

    public Tilemap tileMap;

    int waveNumber = 1;

    float remainingWaveTime = 0f;

    float remainingSpawnTime = 0f;

    void Start()
    {
        remainingWaveTime = waveInterval;
    }

    void Update()
    {
        if (Game.isPaused)
            return;

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
        var currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        var spawnInterval = 3f / waveNumber;

        if (currentNumberOfEnemies < getMinimumAmount())
        {
            return spawnInterval / 4;
        }

        return spawnInterval;
    }

    int getMinimumAmount()
    {
        return 5 + waveNumber * 3;
    }

    void SpawnEnemy()
    {
        var randomPosition = GetRandomPositionOnTileMap();
        var enemyPrefab = GetRandomEnemyPrefab();
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

    GameObject GetRandomEnemyPrefab()
    {
        var randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    Vector3 GetRandomPositionOnTileMap()
    {
        var randomX = Random.Range(tileMap.cellBounds.xMin + 1, tileMap.cellBounds.xMax - 1);
        var randomY = Random.Range(tileMap.cellBounds.yMin + 1, tileMap.cellBounds.yMax - 1);

        var randomPosition = new Vector3Int(randomX, randomY, 0);

        return tileMap.CellToWorld(randomPosition);
    }
}
