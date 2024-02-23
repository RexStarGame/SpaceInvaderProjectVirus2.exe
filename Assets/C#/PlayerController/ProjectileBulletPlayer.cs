using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    public float moveSpeed;
    public float bulletLifetime = 2.0f; // Adjust this value to set the lifetime of the bullet
    public GameObject explosionPrefab;
    private KillScore killScoreManager;

    void Start()
    {
        killScoreManager = GameObject.Find("KillManager").GetComponent<KillScore>();
        Destroy(gameObject, bulletLifetime); // Schedule destruction after bulletLifetime seconds
    }

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HandleEnemyCollision(collision.gameObject);
        }
        else if (collision.gameObject.tag == "WallBoundary")
        {
            DestroyBullet();
        }
    }

    void HandleEnemyCollision(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(PlayerController.damage);
            Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);

            if (enemyHealth.IsDead())
            {
                Destroy(enemy);
                killScoreManager.UpdateScore(1);
            }
        }

        DestroyBullet();
    }

    void DestroyBullet()
    {
    
        Destroy(gameObject);
    }
}
