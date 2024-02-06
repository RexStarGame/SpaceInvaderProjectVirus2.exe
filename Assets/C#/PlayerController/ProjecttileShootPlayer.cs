using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjecttileShootPlayer : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float delayOnShoot = 4;
    public float defaultDelayOnShoot = 4;
    public AudioSource shootAudio;

    // Add a reference to the UI slider
    public Slider shootingVolumeSlider;

    void Start()
    {
        // Set the default value for the UI slider
        if (shootingVolumeSlider != null)
        {
            shootingVolumeSlider.value = shootAudio.volume;
        }
    }

    void Update()
    {
        delayOnShoot -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && delayOnShoot < 0)
        {
            delayOnShoot = defaultDelayOnShoot;
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Play the shoot sound with the adjusted volume
            if (shootAudio != null)
            {
                shootAudio.volume = shootingVolumeSlider != null ? shootingVolumeSlider.value : 1f;
                shootAudio.Play();
            }
        }
    }

    // Add a method to update the volume when the UI slider value changes
    public void UpdateShootingVolume()
    {
        if (shootAudio != null && shootingVolumeSlider != null)
        {
            shootAudio.volume = shootingVolumeSlider.value;
        }
    }
}
