using TMPro;
using UnityEngine;



public class TimerController : MonoBehaviour
{
    public TMP_Text timerText;
    public GameObject gameOverPanel;
    public TMP_InputField playerNameInput;

    public  float gameTime;
    private bool isGameOver = false;

    public void Start()
    {
        gameTime = 0;
    }
    public void Update()
    {
        if (!isGameOver)
        {
            gameTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        float minutes = Mathf.FloorToInt(gameTime / 60);
        float seconds = Mathf.FloorToInt(gameTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);

        playerNameInput.Select();
        playerNameInput.ActivateInputField();

        // Find the KillScore instance in the scene
        KillScore killScoreInstance = FindObjectOfType<KillScore>();

        // Check if KillScore instance exists
     
    }
    public void SaveScoreToLeaderboard()
    {

    }
}