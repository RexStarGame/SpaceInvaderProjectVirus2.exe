using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private List<GameObject> activePowerUps = new List<GameObject>();

    public void AddPowerUp(GameObject powerUp)
    {
        activePowerUps.Add(powerUp);
    }

    public void RemovePowerUp(GameObject powerUp)
    {
        activePowerUps.Remove(powerUp);
    }

    public void DestroyAllPowerUps()
    {
        foreach (GameObject powerUp in activePowerUps)
        {
            // Check if the power-up is not null and has not been destroyed
            if (powerUp != null && powerUp.activeSelf)
            {
                Debug.Log("Destroying PowerUp: " + powerUp.name);
                Destroy(powerUp);
            }
        }

        activePowerUps.Clear();
    }
}