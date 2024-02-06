using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerEmptyGameObject : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject[] aimingPoints;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Get the BulletType2Enemy script from the spawned bullet
        BulletType2Enemy bulletScript = bullet.GetComponent<BulletType2Enemy>();

        if (bulletScript != null)
        {
            // Choose a random aiming point
            GameObject randomAimingPoint = aimingPoints[Random.Range(0, aimingPoints.Length)];

            // Assign the random aimingPoint to the spawned bullet
            bulletScript.SetAimingPoint(randomAimingPoint);
        }
    }
}