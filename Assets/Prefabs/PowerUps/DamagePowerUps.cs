using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUps : MonoBehaviour
{
    [SerializeField]
    private float damageAmount = 0.5f;
    [SerializeField]
    private float damageDuration = 10f;
    [SerializeField]
    private GameObject artToDisable = null;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerShip = other.gameObject.GetComponent<PlayerController>();
        if (playerShip != null)
        {
            StartCoroutine(PowerUpsReloadTimer(playerShip));
        }
    }

    public IEnumerator PowerUpsReloadTimer(PlayerController playerShip)
    {
        // Soft disable
        _collider.enabled = false;
        artToDisable.SetActive(false);

        // Activate power-ups
        ActivatePowerUps(playerShip);

        yield return new WaitForSeconds(damageDuration);

        // Deactivate power-ups 
        DeactivatePowerUps(playerShip);

        // Mark the power-up as collected


        // Destroy the power-up GameObject
        Debug.Log("About to destroy DamagePowerUps");
    }

    public void ActivatePowerUps(PlayerController playerShip)
    {
        playerShip.SetDamage(damageAmount);
    }

    public void DeactivatePowerUps(PlayerController playerShip)
    {
        playerShip.SetDamage(-damageAmount);
    }
}
