using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ReloadSpeedPowerUps : MonoBehaviour
{
    [SerializeField]
    private float reloadSpeedAmount = 1.0f;
    [SerializeField]
    private float powerUpsDuration = 10f;
    [SerializeField]
    private GameObject artToDisable = null;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ProjecttileShootPlayer playerShip = other.gameObject.GetComponent<ProjecttileShootPlayer>();
        if (playerShip != null)
        {
            StartCoroutine(PowerUpsReloadTimer(playerShip));
        }
    }

    public IEnumerator PowerUpsReloadTimer(ProjecttileShootPlayer playerShip)
    {
        // Soft disable
        _collider.enabled = false;
        artToDisable.SetActive(false);

        // Activate power-ups
        ActivatePowerUps(playerShip);

        yield return new WaitForSeconds(powerUpsDuration);

        // Deactivate power-ups 
        DeactivatePowerUps(playerShip);

        // Mark the power-up as collected
        gameObject.SetActive(false);

        // Destroy the power-up GameObject
        Debug.Log("About to destroy DamagePowerUps");
    }

    public void ActivatePowerUps(ProjecttileShootPlayer playerShip)
    {
        playerShip.SetReloadSpeed(reloadSpeedAmount);
    }

    public void DeactivatePowerUps(ProjecttileShootPlayer playerShip)
    {
        playerShip.SetReloadSpeed(-reloadSpeedAmount);
    }
}
