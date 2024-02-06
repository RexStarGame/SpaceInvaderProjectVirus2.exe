using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;





public class ShipShooting : MonoBehaviour
{
    public float moveSpeed;
    public GameObject explostionPrefab;
    private KillScore KillScoreManager;
    public static EnemyHealth health;
    
    // Start is called before the first frame update
    void Start()
    {
        KillScoreManager = GameObject.Find("KillManager").GetComponent<KillScore>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1); // Adjust the damage amount as needed
                Instantiate(explostionPrefab, transform.position, Quaternion.identity);
                if (enemyHealth.IsDead())
                {
                    // Enemy is dead, handle scoring and other actions
                    Destroy(collision.gameObject);
                    KillScoreManager.UpdateScore(1);
                    Instantiate(explostionPrefab, transform.position, Quaternion.identity);
                }
            }

            // Always destroy the player's ship regardless of the enemy's state
            Destroy(gameObject);
        }


        if (collision.gameObject.tag == "WallBoundry")
        {
            Destroy(gameObject);
        }
    }

}
