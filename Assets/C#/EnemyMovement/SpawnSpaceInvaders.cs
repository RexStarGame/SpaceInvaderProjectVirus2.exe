using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpaceInvaders : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Array of enemy prefabs
    public float minSpawnInterval = 10f;  // Minimum time interval between spawns in seconds
    public float maxSpawnInterval = 15f;  // Maximum time interval between spawns in seconds
    public float minDistanceBetweenEnemies = 2f;  // Minimum distance between spawned enemies
    public Transform spawnArea;  // The transform representing the spawn area

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy prefabs array is not assigned or empty!");
            return;
        }

        // Check if there are enemies already within the spawn area
        if (!IsEnemyWithinSpawnArea())
        {
            // Randomly select an enemy prefab from the array
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Set the spawn position to the desired location
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2f, spawnArea.position.x + spawnArea.localScale.x / 2f),
                Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2f, spawnArea.position.y + spawnArea.localScale.y / 2f),
                Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2f, spawnArea.position.z + spawnArea.localScale.z / 2f)
            );

            // Instantiate a new enemy at the specified position
            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsEnemyWithinSpawnArea()
    {
        Collider[] colliders = Physics.OverlapBox(spawnArea.position, spawnArea.localScale / 2f, Quaternion.identity, LayerMask.GetMask("Enemy"));
        return colliders.Length > 0;
    }
}