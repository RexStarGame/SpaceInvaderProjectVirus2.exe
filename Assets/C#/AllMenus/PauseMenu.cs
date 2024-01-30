using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject initiallySelectedObject; // Assign your initial menu item to this field in the Unity Editor
    private GameObject[] menuButtons; // Array to hold all menu buttons
    private int selectedButtonIndex = 0; // Index of the currently selected button

    public void Start()
    {
        // Populate the array with all menu buttons
        menuButtons = new GameObject[]
        {
            // Add your buttons here
            initiallySelectedObject,
            // Add more buttons if needed
        };

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
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        // Check for arrow keys and 'E' key when the game is paused
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Handle up arrow key (e.g., navigate up in your menu)
                // Add your logic here
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Handle down arrow key (e.g., navigate down in your menu)
                // Add your logic here
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                // Handle 'E' key (e.g., confirm selection in your menu)
                // Add your logic here
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;
        // Set the initial selected button
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;
    }
}
