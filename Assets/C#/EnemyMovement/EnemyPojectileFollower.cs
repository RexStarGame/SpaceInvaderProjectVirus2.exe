using UnityEngine;

public class EnemyProjectileFollower : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player using fixedDeltaTime
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.fixedDeltaTime, Space.World);

            // Rotate towards the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            // Destroy the bullet when hit by the player's bullet
            Destroy(gameObject);
        }
    }
}
