using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TMP_Text gameOverText;

    // Call this method when the player loses or decides to end the game.
    public void ShowGameOver(float playerTime)
    {
        // Display game over text.
        gameOverText.text = "Game Over!\nTime: " + playerTime.ToString("F2") + "s";

        // Save player time to leaderboard.
        

        // Other logic for game over menu...
        // For example, you might display additional information or options.
    }

    public void RestartGame()
    {
        
        
            float playerTime = CalculatePlayerTime();

           


            // Get the current active scene
            Scene currentScene = SceneManager.GetActiveScene();

            // Reload the current scene
            SceneManager.LoadScene(currentScene.name);

            Time.timeScale = 1.0f;

        
        // Assuming you have a way to calculate the player's time, replace CalculatePlayerTime() with your actual logic.
       
    }

    // Replace this method with your actual logic to calculate player time.
    private float CalculatePlayerTime()
    {
        // Your logic to calculate player time.
        // For example, you might use Time.timeSinceLevelLoad for an endless space invader type.
        return Time.timeSinceLevelLoad;
    }
}
