using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject[] initiallySelectedObject;
    private GameObject[] menuButtons;
    private int selectedButtonIndex = 0;
    private float inputCooldown = 0.2f; // Adjust this value to control input responsiveness
    private float inputCooldownTimer;

    public bool isGameOverActive;
    public GameObject gameOverChecker;
    public GameObject mainMenuChecker;
    public GameObject leaderboardChecker;

    public void Start()
    {
        isGameOverActive = false;
        menuButtons = new GameObject[]
        {
            initiallySelectedObject[0],
        };

        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);
        }

        SetButtonNavigation();
    }

    private void SetButtonNavigation()
    {
        if (menuButtons.Length > 1)
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                Navigation buttonNavigation = new Navigation();
                buttonNavigation.mode = Navigation.Mode.Explicit;

                int nextIndex = (i + 1) % menuButtons.Length;
                int prevIndex = (i - 1 + menuButtons.Length) % menuButtons.Length;

                buttonNavigation.selectOnUp = menuButtons[prevIndex].GetComponent<Button>();
                buttonNavigation.selectOnDown = menuButtons[nextIndex].GetComponent<Button>();

                menuButtons[i].GetComponent<Button>().navigation = buttonNavigation;
            }
        }
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel") && gameOverChecker.activeSelf == false && mainMenuChecker.activeSelf == false && leaderboardChecker.activeSelf == false) // checks if any menus are active before the player can open the pause menu.
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (isPaused)
        {
            HandleArrowInput();
            HandleEnterKey();
        }
    }

    private void HandleArrowInput()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0.5f && Time.time > inputCooldownTimer)
        {
            ChangeSelectedButton(-1);
            inputCooldownTimer = Time.time + inputCooldown;
        }
        else if (verticalInput < -0.5f && Time.time > inputCooldownTimer)
        {
            ChangeSelectedButton(1);
            inputCooldownTimer = Time.time + inputCooldown;
        }
    }

    private void HandleEnterKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Confirm selection in your menu
            // Add your logic here
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void GameOver()
    {
        isGameOverActive = true;
    }

    private void ChangeSelectedButton(int direction)
    {
        EventSystem.current.SetSelectedGameObject(null);

        // If pressing up arrow and already on the first button, wrap around to the last button
        if (direction == -1 && selectedButtonIndex == 0)
        {
            selectedButtonIndex = menuButtons.Length - 1;
        }
        else
        {
            // Otherwise, update the selected button based on the direction
            selectedButtonIndex = (selectedButtonIndex + direction + menuButtons.Length) % menuButtons.Length;
        }

        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);
    }
}
