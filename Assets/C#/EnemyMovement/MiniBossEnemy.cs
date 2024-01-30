using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject defaultBulletPrefab;
    public GameObject specialBulletPrefab; // The different bullet prefab
    public Transform[] firePoints;
    public float minTimeBetweenShots = 4f;
    public float maxTimeBetweenShots = 10f;
    public float specialBulletChance = 0.2f; // 20% chance to spawn a special bullet

    private void Start()
    {
        InvokeRepeating("Shoot", Random.Range(0f, maxTimeBetweenShots), Random.Range(minTimeBetweenShots, maxTimeBetweenShots));
    }

    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            // Determine if the bullet should be a special bullet
            bool spawnSpecialBullet = Random.Range(0f, 1f) < specialBulletChance;

            // Choose the appropriate bullet prefab
            GameObject bulletPrefab = spawnSpecialBullet ? specialBulletPrefab : defaultBulletPrefab;

            // Instantiate bullet at the current fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Destroy the bullet after a certain time (adjust as needed)
            Destroy(bullet, 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("WallBoundary2"))
        {
            // Destroy the enemy if it collides with player or wall
            Destroy(gameObject);
        }
    }
}
