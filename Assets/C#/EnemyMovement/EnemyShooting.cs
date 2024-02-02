using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
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

            // If the bullet is a special bullet, aim at the player's position
            if (spawnSpecialBullet)
            {
                AimAtPlayer(bulletPrefab, firePoint);
            }
            else
            {
                // Instantiate bullet at the current fire point without aiming
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Destroy(bullet, 5f);
            }
        }
    }

    void AimAtPlayer(GameObject bulletPrefab, Transform firePoint)
    {
        Vector2 playerPosition = PlayerPosition();
        Vector2 direction = (playerPosition - (Vector2)firePoint.position).normalized;

        // Calculate the rotation angle towards the player's position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Instantiate the special bullet at the current fire point with the calculated rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Destroy(bullet, 5f);
    }

    Vector2 PlayerPosition()
    {
        // Add code to get the player's position, such as finding the player GameObject
        // Example: return GameObject.FindGameObjectWithTag("Player").transform.position;
        return Vector2.zero; // Replace this line with the actual code to get the player's position
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