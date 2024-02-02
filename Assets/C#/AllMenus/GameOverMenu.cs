using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public bool isActive;
    public TMP_Text gameOverText;
    private PauseMenu pauseMenu; // calling our pauseMenu
    public GameObject restartButton; // Assign your restart button to this field in the Unity Editor
    // Add more buttons if needed
    private GameObject[] menuButtons; // Array to hold all menu buttons
    private int selectedButtonIndex = 0; // Index of the currently selected button

  

    public void Start()
    {
        // Populate the array with all menu buttons
        menuButtons = new GameObject[]
        {
            restartButton,
            // Add more buttons if needed
        };
        SetInitialSelectedButton();
        // Set the initial selected button
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);

        // Ensure the selected game object is not null
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            // If null, set it to the first button
            EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (isActive)
        {
            // Use arrow keys to navigate through menu items
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeSelectedButton(-1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeSelectedButton(1);
            }

            
        }
        // Press 'F' to click the selected button
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.KeypadEnter)) // checks if input enter eller arrowdown is pressed. 
        {
            SelectCurrentButton();

        }
        
    }

    private void SelectCurrentButton()
    {
        Debug.Log("Selecting current button");
        EventSystem.current.SetSelectedGameObject(null); // Deselect the current button
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]); // Select the current button
        Debug.Log("Selected current button");
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
    

    // Method to handle changing the selected button
    private void ChangeSelectedButton(int direction)
    {
        EventSystem.current.SetSelectedGameObject(null); // Deselect the current button

        // Update the selected button index based on the direction
        selectedButtonIndex = (selectedButtonIndex + direction + menuButtons.Length) % menuButtons.Length;

        // Set the new selected button
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);

        // Debug information
        Debug.Log("Changed selected button to index: " + selectedButtonIndex);
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

    private void SetInitialSelectedButton()
    {
        if (menuButtons.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);
        }
    }
}
