using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            // Destroy the enemy's bullet when hit by the player's bullet
            Destroy(other.gameObject);

            // Destroy the player's bullet
            Destroy(gameObject);
        }
    }
}