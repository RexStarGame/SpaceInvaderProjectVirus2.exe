using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLifes : MonoBehaviour
{
    public int lives = 3;
    public Image[] livesUI;
    public GameObject explosionPrefab;
    //GameOverPanel
    public GameObject gameOverPanel;

    public KillScore KillsManager;
    public string playerName = "DefaultPlayer";
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            lives -= 1;
            for(int i = 0; i < livesUI.Length; i++)
            {
                if(i < lives)
                {
                    livesUI[i].enabled = true;
                }
                else
                {
                    livesUI[i].enabled = false;
                }
            }
            if(lives <= 0)
            {
                
                Debug.Log("GameObject Destoryed");
                Destroy(gameObject);
                Debug.Log("Game Over!");
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);

                //Call the HighScoreUpdate
                KillsManager.HighScoreUpdate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            Destroy(collision.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            lives -= 1;
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
            if (lives <= 0)
            {
                Debug.Log("GameObject Destoryed");
                Destroy(gameObject);
                Debug.Log("Game Over!");
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);

                //Call the HighScoreUpdate
                KillsManager.HighScoreUpdate();
            }
        }
    }
}

