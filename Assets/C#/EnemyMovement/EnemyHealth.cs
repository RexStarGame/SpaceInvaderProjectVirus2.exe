using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float initialHealth = 5f;

    private float currentHealth;
    public UnityEngine.UI.Slider healthSlider;

    // Event to notify other scripts about changes in health
    public event System.Action<float> OnHealthChanged;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initialHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = initialHealth;
            healthSlider.value = currentHealth;
        }
    }

    // Example: Deduct health when the enemy is damaged
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Notify subscribers about the change in health
        OnHealthChanged?.Invoke(currentHealth);

        // Example: Check if the enemy is dead after taking damage
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    // Example: Check if the enemy is dead
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
