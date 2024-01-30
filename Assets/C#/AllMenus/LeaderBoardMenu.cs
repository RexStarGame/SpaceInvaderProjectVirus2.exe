using UnityEngine;
using UnityEngine.EventSystems;

public class LeaderboardMenu : MonoBehaviour
{
    public GameObject leaderboardMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    private GameObject lastActiveMenu;

    public GameObject[] leaderboardButtons; // Array to hold all leaderboard menu buttons
    private int selectedButtonIndex = 0; // Index of the currently selected button

    public void Start()
    {
        // Set the default active menu
        SetMenusActive(false, false, false);
        lastActiveMenu = gameOverMenu;  // Set the default last active menu

        // Set the initial selected button for the leaderboard menu
        SetInitialSelectedButton();
    }

    private void SetInitialSelectedButton()
    {
        if (leaderboardButtons.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(leaderboardButtons[selectedButtonIndex]);
        }
    }

    public void Update()
    {
        // Check if no button is currently selected
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            // Call the method to set the initial selected button
            SetInitialSelectedButton();
        }

        // Check for arrow keys when the leaderboard menu is active
        if (leaderboardMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeSelectedButton(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeSelectedButton(1);
            }
            // Add logic for other input keys if needed

            // Check if 'E' key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Force the selection of the current button
                EventSystem.current.SetSelectedGameObject(null); // Deselect the current button
                EventSystem.current.SetSelectedGameObject(leaderboardButtons[selectedButtonIndex]);
            }
        }
    }

    private void ChangeSelectedButton(int direction)
    {
        EventSystem.current.SetSelectedGameObject(null); // Deselect the current button

        // Update the selected button index based on the direction
        selectedButtonIndex = (selectedButtonIndex + direction + leaderboardButtons.Length) % leaderboardButtons.Length;

        // Set the new selected button
        EventSystem.current.SetSelectedGameObject(leaderboardButtons[selectedButtonIndex]);
    }

    public void LoadLeaderboardMenu()
    {
        // Set the last active menu to false
        lastActiveMenu.SetActive(false);

        // Activate the leaderboard menu
        leaderboardMenu.SetActive(true);
        lastActiveMenu = leaderboardMenu;  // Update the last active menu
        Time.timeScale = 0f; // Pause the game

        // Set the initial selected button for the leaderboard menu
        SetInitialSelectedButton();
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