using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField]
    private PowerUpManager _powerUpManager;
    private Vector3 _PowerUpSpawnPosition;
    private bool _enemyDead = false;
  

    // Start is called before the first frame update
    void Start()
    {
      _powerUpManager = GameObject.Find("PowerUpsSpawner").GetComponent<PowerUpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime);
        if (transform.position.y < -9)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUps")
        {
            Destroy(other.gameObject);
            _PowerUpSpawnPosition = transform.position;
            _enemyDead = true;


            SpawnPowerUp();
            Destroy(this.gameObject, 0.4f);




        }
    }
    public void SpawnPowerUp()
    {
        if(_enemyDead == true)
        {
            int randomPowerUp = Random.Range(0, 7);
           // Instantiate(_powerUpManager._powerUps[randomPowerUp],_PowerUpSpawnPosition, Quaternion.identity);
        }
    }
}
