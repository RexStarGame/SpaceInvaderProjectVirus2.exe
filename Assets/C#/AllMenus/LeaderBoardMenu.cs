using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeaderboardMenu : MonoBehaviour
{
    public GameObject leaderboardMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject gameMenu;
    private GameObject lastActiveMenu;

    public GameObject[] leaderboardButtons; // Array to hold all leaderboard menu buttons
    private int selectedButtonIndex = 0; // Index of the currently selected button

    private bool isCooldownActive = false;
    public float cooldownDuration = 0.2f; // Adjust the duration as needed

    

    public void Start()
    {
        // Set the default active menu
        SetMenusActive(false, false, false);
        lastActiveMenu = gameOverMenu;  // Set the default last active menu

        // Set the initial selected button for the leaderboard menu
        SetInitialSelectedButton();
    }
    public void LoadLeaderboard()
    {
        // Toggle visibility of UI elements
        gameMenu.SetActive(false); // Assuming gameMenu is your main game UI
        leaderboardMenu.SetActive(true);
        // Set the last active menu to false
        lastActiveMenu.SetActive(false);
        // Time.timeScale should be set based on your game's requirements
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
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !isCooldownActive)
            {
                ChangeSelectedButton(1);
                StartCoroutine(StartCooldown());
            }
            // Add logic for other input keys if needed

            // Check if 'DownArrow' key is pressed
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
            {
                // Force the selection of the current button
                EventSystem.current.SetSelectedGameObject(null); // Deselect the current button
                EventSystem.current.SetSelectedGameObject(leaderboardButtons[selectedButtonIndex]);
            }

            // Check if 'G' key is pressed
            CheckGKeyInput(); // Call the method to handle 'G' key press
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
        leaderboardMenu.SetActive(false);
        // Return to the last active menu
        SetMenusActive(false, false, false);
        gameMenu.SetActive(true); //return to game
        Time.timeScale = 0f; // Unpause the game
    }

    public void ReturnToLeaderBoard()
    {
        // Return to the last active menu
        SetMenusActive(false, false, false);
        leaderboardMenu.SetActive(true); //return to game
        Time.timeScale = 0f; // Unpause the game
    }

    private void SetMenusActive(bool leaderboardActive, bool gameMenuaktiv, bool pauseActive)
    {
        leaderboardMenu.SetActive(leaderboardActive);
        gameMenu.SetActive(gameMenuaktiv);
        pauseMenu.SetActive(pauseActive);
    }

    private IEnumerator StartCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(cooldownDuration);
        isCooldownActive = false;
    }
    void CheckGKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.G) && EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.Log("G Key Pressed");

            // Simulate a button click
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }
    }
}