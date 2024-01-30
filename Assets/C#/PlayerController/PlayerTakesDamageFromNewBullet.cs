using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakesDamageFromNewBullet : MonoBehaviour
{
    public int lives = 3;
    public Image[] livesUI;
    public GameObject explosionPrefab;
    public GameObject gameOverPanel;
    public KillScore KillsManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            Destroy(collision.gameObject); // Destroy the enemy bullet
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            lives -= 1;

            UpdateLivesUI();

            if (lives <= 0)
            {
                Debug.Log("Game Over!");
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);

                // Call the HighScoreUpdate
                KillsManager.HighScoreUpdate();
            }
        }
    }

    private void UpdateLivesUI()
    {
        for (int i = 0; i < livesUI.Length; i++)
        {
            if (i < lives)
            {
                livesUI[i].enabled = true;
            }
            else
            {
                livesUI[i].enabled = false;
            }
        }
    }
}
