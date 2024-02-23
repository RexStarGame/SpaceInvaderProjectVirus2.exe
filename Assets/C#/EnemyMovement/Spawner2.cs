using System.Collections;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Array of enemy prefabs
    public float minSpawnInterval = 10f;  // Minimum time interval between spawns in seconds
    public float maxSpawnInterval = 15f;  // Maximum time interval between spawns in seconds
    public float minDistanceBetweenEnemies = 2f;  // Minimum distance between spawned enemies
    public Transform spawnArea;  // The transform representing the spawn area
    public int maxEnemiesAlive = 3;  // Maximum number of enemies allowed to be alive simultaneously
    public float countdownTime = 5f;  // Countdown timer duration in seconds

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            int aliveEnemies = CountAliveEnemies();

            if (aliveEnemies < maxEnemiesAlive)
            {
                float countdown = countdownTime;

                // Start countdown timer
                while (countdown > 0)
                {
                    yield return new WaitForSeconds(1f);
                    countdown -= 1f;
                }

                // Randomly select an enemy prefab from the array
                GameObject randomEnemyPrefab = GetRandomEnemyPrefab();

                // Set the spawn position to the desired location
                Vector3 spawnPosition = GetRandomSpawnPosition();

                // Instantiate a new enemy at the specified position
                Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    int CountAliveEnemies()
    {
        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        return aliveEnemies.Length;
    }

    GameObject GetRandomEnemyPrefab()
    {
        // Implement rarity logic here if needed
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Set the spawn position to the desired location
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2f, spawnArea.position.x + spawnArea.localScale.x / 2f),
            Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2f, spawnArea.position.y + spawnArea.localScale.y / 2f),
            Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2f, spawnArea.position.z + spawnArea.localScale.z / 2f)
        );

        return spawnPosition;
    }
}
