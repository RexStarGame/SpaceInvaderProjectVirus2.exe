using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public bool isActive;
    public TMP_Text gameOverText;
    

    public GameObject restartButton; // Assign your restart button to this field in the Unity Editor

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            // Use arrow keys to navigate through menu items
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                EventSystem.current.SetSelectedGameObject(null); // Deselect the current button
                EventSystem.current.SetSelectedGameObject(restartButton); // Select the restart button
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                EventSystem.current.SetSelectedGameObject(null); // Deselect the current button
                // Add logic to select other buttons if needed
            }

            // Press 'E' to click the selected button
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExecuteEvents.Execute(restartButton, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }
    }

    // Call this method when the player loses or decides to end the game.
    public void ShowGameOver(float playerTime)
    {
        // Display game over text.
        gameOverText.text = "Game Over!\nTime: " + playerTime.ToString("F2") + "s";

        // Save player time to leaderboard.
        // Other logic for game over menu...
        // For example, you might display additional information or options.

        isActive = true; // Enable navigation and button activation
    }

    public void RestartGame()
    {
       

        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);

        Time.timeScale = 1.0f;

        // Assuming you have a way to calculate the player's time, replace CalculatePlayerTime() with your actual logic.
    }

    // Replace this method with your actual logic to calculate player time.
 
}