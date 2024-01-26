using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileShootPlayer : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float delayOnShoot = 4;
    public float defulDelayonShoot = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        delayOnShoot -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && delayOnShoot < 0)
        {
            delayOnShoot = defulDelayonShoot;
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}
