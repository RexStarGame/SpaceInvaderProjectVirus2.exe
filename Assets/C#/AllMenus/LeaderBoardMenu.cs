using System.Collections;
using UnityEngine;

public class LeaederBoardMenu : MonoBehaviour
{
    public GameObject leaderboardMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    private GameObject lastActiveMenu;

    void Start()
    {
        // Set the default active menu
        SetMenusActive(false, false, false);
        lastActiveMenu = gameOverMenu;  // Set the default last active menu
    }

    public void LoadLeaderboardMenu()
    {
        // Set the last active menu to false
        lastActiveMenu.SetActive(false);

        // Activate the leaderboard menu
        leaderboardMenu.SetActive(true);
        lastActiveMenu = leaderboardMenu;  // Update the last active menu
        Time.timeScale = 0f; // Pause the game
    }

    public void ReturnToLastMenu()
    {
        // Return to the last active menu
        SetMenusActive(false, false, false);
        lastActiveMenu.SetActive(true);
        Time.timeScale = 0f; // Unpause the game
    }

    private void SetMenusActive(bool leaderboardActive, bool gameOverActive, bool pauseActive)
    {
        leaderboardMenu.SetActive(leaderboardActive);
        gameOverMenu.SetActive(gameOverActive);
        pauseMenu.SetActive(pauseActive);
    }
}
