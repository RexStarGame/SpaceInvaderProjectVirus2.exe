using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuGame : MonoBehaviour
{
    private bool isPaused;
    public GameObject GameMenu;
    private Transform menuButtonsParent; // Parent object containing the buttons
    private Button[] menuButtons; // Array to store the buttons
    private int selectedButtonIndex = 0;
    private bool isInputInProcess = false;

    void Awake()
    {
        FindMenuButtons();
        SetInitialSelectedButton();
    }

    void Update()
    {
        Debug.Log("Selected Object: " + EventSystem.current.currentSelectedGameObject);

        // Check for arrow keys and 'G' key when the game is paused
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

        // Check if 'W' key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W Key Pressed");
            ChangeSelectedButton(-1);
        }
        // Check if 'S' key is pressed
        else if (Input.GetKeyDown(KeyCode.S) && isPaused)
        {
            Debug.Log("S Key Pressed");
            SelectCurrentButton();
        }

        // Check if 'G' key is pressed
        else if (Input.GetKeyDown(KeyCode.G) && isPaused)
        {
            Debug.Log("G Key Pressed");
            SelectCurrentButton();
        }
    }

    IEnumerator InputCooldown()
    {
        isInputInProcess = true;
        yield return new WaitForSecondsRealtime(1f); // Adjust the delay duration if needed
        isInputInProcess = false;
    }

    void FindMenuButtons()
    {
        // Assuming your buttons are direct children of the GameMenu object
        menuButtonsParent = GameMenu.transform;
        menuButtons = menuButtonsParent.GetComponentsInChildren<Button>();

        foreach (Button button in menuButtons)
        {
            Debug.Log("Button Name: " + button.name);
        }

        Debug.Log("Found " + menuButtons.Length + " buttons.");
    }

    void SelectCurrentButton()
    {
        Debug.Log("Selecting current button");

        if (GameMenu.activeSelf)
        {
            if (menuButtons != null && menuButtons.Length > 0)
            {
                Button selectedButton = menuButtons[selectedButtonIndex];

                if (selectedButton != null)
                {
                    StartCoroutine(InputCooldown());
                    selectedButton.onClick.Invoke();
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                {
                    Debug.LogWarning("No selected button found.");
                }
            }
            else
            {
                Debug.LogWarning("No buttons found. Make sure buttons are in the hierarchy under the GameMenu object.");
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
        yield return null;

        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        if (GameMenu.activeSelf && selectedButton != null)
        {
            Selectable selectable = selectedButton.GetComponent<Selectable>();

            if (selectable != null && selectable.IsActive() && selectable.IsInteractable())
            {
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
            if (GameMenu.activeSelf)
            {
                Debug.LogWarning("No selected button found.");
            }
        }
    }

    public void MainMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 0;
        GameMenu.SetActive(true);
        isPaused = true;

        FindMenuButtons();
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
        EventSystem.current.SetSelectedGameObject(null);

        if (GameMenu.activeSelf)
        {
            if (menuButtons != null && menuButtons.Length > 0)
            {
                selectedButtonIndex = (selectedButtonIndex + direction + menuButtons.Length) % menuButtons.Length;

                EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex].gameObject);

                HighlightSelectedButton();
            }
            else
            {
                Debug.LogWarning("No buttons found. Make sure buttons are in the hierarchy under the GameMenu object.");
            }
        }
        else
        {
            Debug.Log("Main menu is not active. Cannot change selected button.");
        }
    }

    void SetInitialSelectedButton()
    {
        if (menuButtons.Length == 0)
        {
            Debug.LogWarning("No buttons found in the hierarchy under the GameMenu object.");
            return;
        }

        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex].gameObject);

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Debug.LogWarning("Selected button is null.");
        }
    }
}
