using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphicsSittings : MonoBehaviour
{
    public TMP_Dropdown antiAliasingDropdown;

    void Start()
    {
        // Initialize the dropdown value based on the current anti-aliasing level
        antiAliasingDropdown.value = QualitySettings.antiAliasing;
        antiAliasingDropdown.onValueChanged.AddListener(OnAntiAliasingDropdownChanged);
    }

    public void OnAntiAliasingDropdownChanged(int value)
    {
        Debug.Log("OnAntiAliasingDropdownChanged called");

        // Get the text from the dropdown
        string dropdownText = antiAliasingDropdown.options[antiAliasingDropdown.value].text;

        // Map dropdown text to corresponding integer value
        int antiAliasingLevel = MapAntiAliasingLevel(dropdownText);

        if (antiAliasingLevel != -1)
        {
            QualitySettings.antiAliasing = antiAliasingLevel;
            Debug.Log("Selected Anti-Aliasing Level: " + antiAliasingLevel);
        }
        else
        {
            // Log an error with the specific text causing the mapping failure
            Debug.LogError("Failed to map Anti-Aliasing level from dropdown. Invalid text: " + dropdownText);
        }
    }
    private int MapAntiAliasingLevel(string dropdownText)
    {
        // Map dropdown text to corresponding integer value
        switch (dropdownText)
        {
            case "Low Graphics":
                return 2; // Adjust this value based on your requirements
            case "Medium Graphics":
                return 4; // Adjust this value based on your requirements
            case "High Graphics":
                return 8; // Adjust this value based on your requirements
                          // Add more cases as needed

            default:
                // Return -1 for unknown or unsupported values
                return -1;
        }
    }
}