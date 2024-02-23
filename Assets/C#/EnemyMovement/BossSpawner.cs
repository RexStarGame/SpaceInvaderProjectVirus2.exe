using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Array of enemy prefabs
    public float minSpawnInterval = 10f;  // Minimum time interval between spawns in seconds
    public float maxSpawnInterval = 15f;  // Maximum time interval between spawns in seconds
    public float minDistanceBetweenEnemies = 2f;  // Minimum distance between spawned enemies
    public Transform spawnArea;  // The transform representing the spawn area
    public int killsPerSpawn = 100;  // Number of kills required to spawn an enemy
    public float countdownTime = 5f;  // Countdown timer duration in seconds

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        int remainingKills = 0; // Initialize remaining kills

        while (true)
        {
            int playerKills = KillScore.killScore; // Get current player kills

            // Check if player has enough kills for a spawn
            if (playerKills >= killsPerSpawn)
            {
                float countdown = countdownTime;

                // Start countdown timer
                while (countdown > 0)
                {
                    yield return new WaitForSeconds(1f);
                    countdown -= 1f;
                }

                // Calculate the number of spawns based on remaining kills and killsPerSpawn
                int numberOfSpawns = playerKills / killsPerSpawn;

                // Calculate remaining kills for the next spawn
                remainingKills = playerKills % killsPerSpawn;

                // Spawn enemies based on the number of spawns
                for (int i = 0; i < numberOfSpawns; i++)
                {
                    // Randomly select an enemy prefab from the array
                    GameObject randomEnemyPrefab = GetRandomEnemyPrefab();

                    // Set the spawn position to the desired location
                    Vector3 spawnPosition = GetRandomSpawnPosition();

                    // Instantiate a new enemy at the specified position
                    GameObject enemy = Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);

                    // Ensure the enemy stays within the game world bounds
                    ShipMovement2Boss enemyMovement = enemy.GetComponent<ShipMovement2Boss>();
                    if (enemyMovement != null)
                    {
                       
                    }
                }
            }

            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
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
            0f  // Since Z is always 0
        );

        return spawnPosition;
    }
}
