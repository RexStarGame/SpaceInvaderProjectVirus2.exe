using UnityEngine;


public class BulletType2Enemy : MonoBehaviour
{
    public float speed = 5f;
    private GameObject aimingPoint;

    void Start()
    {
        if (aimingPoint != null)
        {
            // Calculate direction towards the chosen aiming point
            Vector3 direction = (aimingPoint.transform.position - transform.position).normalized;

            // Set bullet velocity
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        else
        {
            Debug.LogError("Aiming point is not assigned. Please assign an empty GameObject to the aimingPoint field.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WallBoundry2")
        {
            Destroy(gameObject);
        }
    }

    public void SetAimingPoint(GameObject newAimingPoint)
    {
        aimingPoint = newAimingPoint;
    }
}