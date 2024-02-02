using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuGame : MonoBehaviour
{
    private bool isPaused;  // Add this line
    public GameObject GameMenu;
    public string buttonTag = "StartGameButton";
    private GameObject[] menuButtons;
    private int selectedButtonIndex = 0;
    private bool isInputInProcess = false;

    void Start()
    {
        // Find all GameObjects with the specified tag
        menuButtons = GameObject.FindGameObjectsWithTag(buttonTag);

        // Set the initial selected button
        SetInitialSelectedButton();
    }

    void Update()
    {
        Debug.Log("Selected Object: " + EventSystem.current.currentSelectedGameObject);

        // Check for arrow keys and 'E' key when the game is paused
        if (isPaused)
        {
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput > 0 && !isInputInProcess)
            {
                Debug.Log("Up Arrow Key Pressed");
                ChangeSelectedButton(-1);
                StartCoroutine(InputCooldown());
            }
            else if (verticalInput < 0 && !isInputInProcess)
            {
                Debug.Log("Down Arrow Key Pressed");
                ChangeSelectedButton(1);
                StartCoroutine(InputCooldown());
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Key Pressed");
            SelectCurrentButton();
        }
    }

    IEnumerator InputCooldown()
    {
        isInputInProcess = true;
        yield return new WaitForSeconds(0.2f); // Adjust cooldown duration if needed
        isInputInProcess = false;
    }

    void SelectCurrentButton()
    {
        Debug.Log("Selecting current button");

        // Check if the main menu is active
        if (GameMenu.activeSelf)
        {
            // Check if the menuButtons array is not null and not empty
            if (menuButtons != null && menuButtons.Length > 0)
            {
                GameObject selectedButton = menuButtons[selectedButtonIndex];

                if (selectedButton != null)
                {
                    EventSystem.current.SetSelectedGameObject(selectedButton);
                    HighlightSelectedButton();
                }
                else
                {
                    Debug.LogWarning("No selected button found.");
                }
            }
            else
            {
                Debug.LogWarning("No buttons found. Make sure buttons are tagged with 'StartGameButton'.");
            }
        }
        else
        {
            Debug.Log("Main menu is not active. Cannot select button.");
        }
    }


    void HighlightSelectedButton()
    {
        StartCoroutine(DelayedHighlight());
    }

    IEnumerator DelayedHighlight()
    {
        yield return null; // Wait for the next frame

        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        if (selectedButton != null && GameMenu.activeSelf == true)
        {
            Selectable selectable = selectedButton.GetComponent<Selectable>();

            if (selectable != null && selectable.IsActive() && selectable.IsInteractable())
            {
                Debug.Log("Selectable found: " + selectable.name);
                selectable.Select();
                selectable.OnSelect(null);
            }
            else
            {
                Debug.LogWarning("Selectable is not active or interactable.");
            }
        }
        else
        {
            Debug.LogWarning("No selected button found.");
        }
    }

    public void MainMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 0;
        GameMenu.SetActive(true);
        isPaused = true;

        // Set the initial selected button
        SetInitialSelectedButton();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        GameMenu.SetActive(false);
        isPaused = false;
    }

    void ChangeSelectedButton(int direction)
    {
        Debug.Log("Changing selected button");
        EventSystem.current.SetSelectedGameObject(null);

        selectedButtonIndex = (selectedButtonIndex + direction + menuButtons.Length) % menuButtons.Length;

        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);
        Debug.Log("Selected Button Index: " + selectedButtonIndex);

        // Call the HighlightSelectedButton method to visually highlight the new selected button.
        HighlightSelectedButton();
    }

    void SetInitialSelectedButton()
    {
        if (menuButtons.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);

            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex]);
            }
        }
    }
}
