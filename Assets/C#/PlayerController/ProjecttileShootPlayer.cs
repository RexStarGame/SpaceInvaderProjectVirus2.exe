using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjecttileShootPlayer : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float delayOnShoot = 0.6f;
    public float defaultDelayOnShoot = 4;
    public float specialAttackCooldown = 15f; // Cooldown for the special attack
    private float lastSpecialAttackTime;
    public float specialAttackDuration = 5f; // Duration of the special attack
    public float cheatDurationTime = 10f; // Duration for the cheat code
    private float lastCheatActivationTime; // Time of the last cheat code activation
    public AudioSource shootAudio;
    public Slider shootingVolumeSlider;
    public TextMeshProUGUI cooldownText;
    public Image cooldownImage; // New variable for the cooldown image
    private bool isCheatCodeActivated = false; // Variable to track cheat code activation
    
    void Start()
    {
        defaultDelayOnShoot = 0.6f;
        // Set the default value for the UI slider
        if (shootingVolumeSlider != null)
        {
            shootingVolumeSlider.value = shootAudio.volume;
        }

        // Ensure cooldownText is assigned
        if (cooldownText == null)
        {
            Debug.LogError("Please assign the TextMeshProUGUI component for cooldownText in the inspector.");
        }

        // Ensure cooldownImage is assigned
        if (cooldownImage == null)
        {
            Debug.LogError("Please assign the Image component for cooldownImage in the inspector.");
        }
    }

    void Update()
    {
        delayOnShoot -= Time.deltaTime;

        // Check if the F key is pressed for regular shooting
        if (Input.GetKey(KeyCode.F) && delayOnShoot < 0)
        {
            delayOnShoot = defaultDelayOnShoot;
            Shoot();
        }

        // Check if the Down Arrow key is pressed for the special attack
        if (Input.GetKeyDown(KeyCode.DownArrow) && Time.time - lastSpecialAttackTime >= specialAttackCooldown)
        {
            lastSpecialAttackTime = Time.time;
            StartCoroutine(ActivateSpecialAttack(specialAttackDuration));
        }

        CheckCheatCode(); // Add this line to check for the cheat code

        UpdateCooldownText();
    }

    private void CheckCheatCode()
    {
        if (IsCheatCodeActivated() && Time.time - lastCheatActivationTime >= cheatDurationTime)
        {
            lastCheatActivationTime = Time.time;
            lastSpecialAttackTime = Time.time - specialAttackCooldown; // Reset special attack cooldown
            StartCoroutine(ActivateSpecialAttack(cheatDurationTime));
            
        }
    }


    bool IsCheatCodeActivated()
    {
        return Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.F);
    }
    void Shoot()
    {
        Debug.Log("Shooting!");
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    public void UpdateShootingVolume()
    {
        if (shootAudio != null && shootingVolumeSlider != null)
        {
            shootAudio.volume = shootingVolumeSlider.value;
        }
    }

    public void SetReloadSpeed(float newReLoadSpeed)
    {
        defaultDelayOnShoot += newReLoadSpeed;
        delayOnShoot += newReLoadSpeed;
    }

    IEnumerator ActivateSpecialAttack(float duration)
    {
        Debug.Log("Special Attack Activated!");

        // Store the original reload speed before applying the cheat
        

        // Check if cheat code is not already activated
        if (!isCheatCodeActivated)
        {
            // Set cheat code activation flag
            isCheatCodeActivated = true;

            // Increase shooting speed during special attack
            SetReloadSpeed(-0.25f);
        }

        // Wait for the special attack duration
        yield return new WaitForSeconds(duration);

        // Restore default shooting speed after the special attack
        SetReloadSpeed(0.25f);

        // Reset cheat code activation flag
        isCheatCodeActivated = false;
    }


    void UpdateCooldownText()
    {
        // Calculate the remaining cooldown time
        float remainingCooldown = Mathf.Max(0f, specialAttackCooldown - (Time.time - lastSpecialAttackTime));

        // Update the TextMeshProUGUI with the remaining cooldown time or "Ready to use"
        if (remainingCooldown > 0)
        {
            cooldownText.text = string.Format("Cooldown: {0:0}s", remainingCooldown);
        }
        else
        {
            cooldownText.text = "Ready to use!";
        }

        // Show the cooldown image only when the cooldown has completed
        cooldownImage.enabled = remainingCooldown <= 0;
    }
}
