using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MainMenuGame : MonoBehaviour
{
    private bool isPaused;
    public GameObject GameMenu;
    private Button[] menuButtons;
    private int selectedButtonIndex = 0;
    private bool isInputInProcess = false;

    void Awake()
    {
        FindMenuButtons();
        SetInitialSelectedButton();
    }

    public void Update()
    {
        // Check if 'G' key is pressed
        if (Input.GetKeyDown(KeyCode.G) && isPaused && !isInputInProcess)
        {
            Debug.Log("G Key Pressed");
            HandleEnterAction();
        }

        // Check for arrow keys or 'W'/'S' keys when the game is paused
        if (isPaused)
        {
            float verticalInput = Input.GetAxis("Vertical");

            if ((verticalInput > 0 || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isInputInProcess)
            {
                Debug.Log("Up Arrow Key or W Key Pressed");
                ChangeSelectedButton(-1);
                StartCoroutine(InputCooldown());
            }
            else if ((verticalInput < 0 || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !isInputInProcess)
            {
                Debug.Log("Down Arrow Key or S Key Pressed");
                ChangeSelectedButton(1);
                StartCoroutine(InputCooldown());
            }
        }
    }

    void HandleEnterAction()
    {
        // Check if 'G' key is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

            if (selectedObject != null)
            {
                Button button = selectedObject.GetComponent<Button>();

                if (button != null)
                {
                    Debug.Log("Button Clicked: " + selectedObject.name);
                    button.onClick.Invoke();
                }
                else
                {
                    Debug.LogWarning("No Button component found on the selected object.");
                }
            }
            else
            {
                Debug.LogWarning("No selected object found.");
            }
        }
    }

    IEnumerator InputCooldown()
    {
        isInputInProcess = true;
        yield return new WaitForSecondsRealtime(1f);
        isInputInProcess = false;
    }

    void FindMenuButtons()
    {
        menuButtons = GameMenu.GetComponentsInChildren<Button>();
    }

    void ChangeSelectedButton(int direction)
    {
        selectedButtonIndex = (selectedButtonIndex + direction + menuButtons.Length) % menuButtons.Length;
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex].gameObject);
    }

    void SetInitialSelectedButton()
    {
        if (menuButtons.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(menuButtons[selectedButtonIndex].gameObject);
        }
    }
}
