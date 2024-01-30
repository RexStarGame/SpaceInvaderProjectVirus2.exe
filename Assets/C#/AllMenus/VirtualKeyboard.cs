using UnityEngine;
using TMPro;

public class VirtualKeyboard : MonoBehaviour
{
    public float buttonCooldown = 0.2f; // Cooldown time in seconds
    private float lastButtonClickTime; // Time of the last button click

    private TMP_InputField inputField; // Reference to your TextMeshPro input field

    public void SetInputField(TMP_InputField field)
    {
        inputField = field;
    }
    public void Start()
    {
        inputField = FindObjectOfType<TMP_InputField>(); // If there's only one TMP_InputField in the scene
    }

    public void OnButtonClick(string letter)
    {

        Debug.Log("Button Clicked: " + letter);
        if (string.IsNullOrEmpty(letter))
        {
            Debug.LogError("Letter parameter is empty!");
            return;
        }
        // Check if enough time has passed since the last button click
        if (Time.unscaledTime - lastButtonClickTime >= buttonCooldown)
        {
            // Update the last button click time
            lastButtonClickTime = Time.unscaledTime;

            // Handle button click
            if (letter == "Backspace")
            {
                // Handle backspace
                if (inputField.text.Length > 0)
                {
                    inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
                }
            }
            else
            {
                // Handle other letters
                inputField.text += letter;
                Debug.Log("Input Field Text: " + inputField.text);
            }
            try
            {
                // Code that might be triggering the BroadcastMessage error
            }
            catch (System.Exception e)
            {
                Debug.LogError("Caught an exception: " + e.Message);
            }
        }
    }
}

