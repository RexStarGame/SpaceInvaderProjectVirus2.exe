using TMPro;
using UnityEngine;


[System.Serializable]
public class TimerController : MonoBehaviour
{
    public TMP_Text timerText;
    public GameObject gameOverPanel;
    public TMP_InputField playerNameInput;

    private float gameTime;
    private bool isGameOver = false;

    void Update()
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
    }

    public void SaveScoreToLeaderboard()
    {

    }
}