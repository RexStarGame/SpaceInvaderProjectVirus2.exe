using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackBehavior : MonoBehaviour
{
    public float damageMultiplier = 1.0f;
    public int baseDamage = 10;
    public float despawnTime = 10f; // Adjust this value for the despawn time
    public float bulletSpeed = 5f;  // Adjust this value for the bullet speed
    public GameObject explosionPrefab;
    public float explosionRadius = 1.0f;  // Edit this value in the Unity Editor
    private float startTime;
    private bool exploded = false;
    private CircleCollider2D explosionCollider;
    private KillScore killScoreManager;
    public float defaultExplosionRadius;
    private HashSet<GameObject> affectedEnemies = new HashSet<GameObject>();

    // Called when the special attack trigger enters a collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is on the "EnemyShip" layer and has the "Enemy" tag
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyShip") && other.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            DealDamageToEnemy(other.gameObject);

            // Add the affected enemy to the list
            affectedEnemies.Add(other.gameObject);

            // Perform the explosion effect
            Explode();

            // Destroy the special attack bullet
            Destroy(gameObject);
        }
    }

    void DealDamageToEnemy(GameObject enemyGameObject)
    {
        // Calculate the damage based on the damageMultiplier
        int calculatedDamage = Mathf.RoundToInt(baseDamage * damageMultiplier);

        // Get the EnemyHealth component of the collided enemy
        EnemyHealth enemyHealth = enemyGameObject.GetComponent<EnemyHealth>();

        // Check if the EnemyHealth component is not null
        if (enemyHealth != null)
        {
            // Deal damage to the enemy
            enemyHealth.TakeDamage(calculatedDamage);

            // If the calculated damage is enough to kill the enemy, destroy it
            if (enemyHealth.IsDead())
            {
                Destroy(enemyGameObject);
                UpdateKillScore(1);
            }
        }
    }

    void Explode()
    {
        // Check if the explosion has already occurred
        if (exploded)
        {
            return;
        }

        // Optionally, you can play particle effects, sound, etc.

        // Instantiate the explosion prefab
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Access the CircleCollider2D component of the explosion prefab
        explosionCollider = explosion.GetComponent<CircleCollider2D>();

        // Set the collider size based on the explosion radius
        if (explosionCollider != null)
        {
            explosionCollider.radius = explosionRadius;
        }

        // Dynamically scale the explosion prefab based on the explosion radius
        float scaleFactor = explosionRadius / defaultExplosionRadius; // Assuming defaultExplosionRadius is the original size of the prefab
        explosion.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);

        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        // Apply damage to the appropriate targets and update the kill score
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                DealDamageToEnemy(collider.gameObject);
            }
        }

        // Mark that the explosion has occurred
        exploded = true;

        // Destroy the special attack bullet after despawnTime seconds
        Destroy(gameObject);
    }

    void UpdateKillScore(int count)
    {
        // Find the KillScore manager in the scene
        if (killScoreManager == null)
        {
            killScoreManager = GameObject.Find("KillManager").GetComponent<KillScore>();
        }

        // Update the kill score based on the count of affected enemies
        if (killScoreManager != null)
        {
            killScoreManager.UpdateScore(count);
        }
    }

    void Start()
    {
        startTime = Time.time;

        // Apply forward force to move the bullet
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.velocity = Vector2.up * bulletSpeed;
        }
    }

    void Update()
    {
        // Check if the special attack should despawn
        if (Time.time - startTime >= despawnTime)
        {
            Destroy(gameObject);
        }
    }
}
