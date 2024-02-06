using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 1f;
    public float bulletSpeed = 5f;
    public float deviationAngle = 30f; // The angle between the down-left and down-right directions

    private void Start()
    {
        InvokeRepeating("Shoot", 0f, shootInterval);
    }

    void Shoot()
    {
        Vector2 shootDirection = GetShootDirection();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        bulletRb.velocity = shootDirection * bulletSpeed;
    }

    Vector2 GetShootDirection()
    {
        float randomAngle = Random.Range(-deviationAngle, deviationAngle);
        Quaternion rotation = Quaternion.Euler(0f, 0f, randomAngle);

        return rotation * Vector2.down;
    }
}