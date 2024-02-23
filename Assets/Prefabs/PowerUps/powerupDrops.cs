using UnityEngine;

public class PowerUpDrops : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUpPrefabs; // Array of power-up prefabs
    [SerializeField]
    private float[] dropChances; // Probability of dropping each power-up (0.0 to 1.0)
    private static PowerUpDrops instance;

    private void Awake()
    {
        Debug.Log("Awake called");
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy called");

        //DropRandomPowerUp();
    }

    /*private void DropRandomPowerUp()
    {
        NormalizeProbabilities();

        float randomValue = Random.value;
        float cumulativeChance = 0f;

        for (int i = 0; i < powerUpPrefabs.Length; i++)
        {
            cumulativeChance += dropChances[i];

            if (randomValue <= cumulativeChance)
            {
                InstantiatePowerUp(powerUpPrefabs[i]);
                break;
            }
        }
    }
    */
    private void NormalizeProbabilities()
    {
        // Normalize drop chances to ensure the sum is 1.0
        float sum = 0f;
        for (int i = 0; i < dropChances.Length; i++)
        {
            sum += dropChances[i];
        }

        // Adjust probabilities if the sum is not already 1.0
        if (sum != 1.0f)
        {
            for (int i = 0; i < dropChances.Length; i++)
            {
                dropChances[i] /= sum;
            }
        }
    }

    private void InstantiatePowerUp(GameObject powerUpPrefab)
    {
        if (powerUpPrefab != null)
        {
            GameObject spawnedPowerUp = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);

            // Attach the cleanup script to the spawned power-up (if needed)
            // ...
        }
    }
}
