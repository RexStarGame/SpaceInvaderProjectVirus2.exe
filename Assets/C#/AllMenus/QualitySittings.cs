using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class QualitySittings : MonoBehaviour
{
    public TMP_Text qualityText;
    public TMP_Dropdown qualityDropdown;

    void Start()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        UpdateQualityText();
    }

    public void OnQualityDropdownChanged()
    {
        int qualityLevel = qualityDropdown.value;
        QualitySettings.SetQualityLevel(qualityLevel);
        UpdateQualityText();
        Debug.Log("Selected Quality Level: " + qualityLevel);
    }

    void UpdateQualityText()
    {
        qualityText.text = "Quality Level: " + QualitySettings.GetQualityLevel().ToString();
    }
}
