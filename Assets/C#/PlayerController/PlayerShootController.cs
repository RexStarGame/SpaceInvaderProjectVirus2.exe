using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PlayerShootController : MonoBehaviour
{
    public GameObject specialAttackPrefab;
    public float specialAttackCooldown = 60f; // 60 seconds cooldown
    private float lastSpecialAttackTime;
    public Image cooldownImage;

    // TextMeshProUGUI for cooldown display
    public TextMeshProUGUI cooldownText;

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed since the last special attack
        float timeSinceLastAttack = Time.time - lastSpecialAttackTime;

        if (timeSinceLastAttack >= specialAttackCooldown)
        {
            // Ready to use
            cooldownText.text = "Ready to Use";
            cooldownImage.enabled = true;  // Show the image
        }
        else
        {
            // On cooldown, display remaining time
            float remainingCooldown = specialAttackCooldown - timeSinceLastAttack;
            cooldownText.text = $"Cooldown: {remainingCooldown:F1}s";
            cooldownImage.enabled = false;  // Hide the image
        }

        // Check if the player presses UpArrow and the special attack is ready
        if (Input.GetKeyDown(KeyCode.UpArrow) && timeSinceLastAttack >= specialAttackCooldown)
        {
            // Instantiate the special attack bullet
            InstantiateSpecialAttack();

            // Update the last special attack time
            lastSpecialAttackTime = Time.time;

            // Update the cooldown text
            UpdateCooldownText();
        }
    }

    void InstantiateSpecialAttack()
    {
        // Instantiate the special attack prefab
        Instantiate(specialAttackPrefab, transform.position, Quaternion.identity);
    }

    void UpdateCooldownText()
    {
        // Check if the TextMeshProUGUI component is assigned
        if (cooldownText != null)
        {
            // Calculate the remaining cooldown time
            float remainingCooldown = Mathf.Max(0f, specialAttackCooldown - (Time.time - lastSpecialAttackTime));

            // Update the TextMeshProUGUI with the remaining cooldown time or "Ready to use"
            if (remainingCooldown > 0)
            {
                cooldownText.text = string.Format("{0:0}s", remainingCooldown);
            }
            else
            {
                cooldownText.text = "Ready to use!";
                StartCoroutine(HideCooldownTextAfterDelay(2f)); // Hide after 2 seconds
            }

            // Show the text
            cooldownText.enabled = true;
        }
    }

    IEnumerator HideCooldownTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Hide the text
        cooldownText.enabled = false;
    }
}
