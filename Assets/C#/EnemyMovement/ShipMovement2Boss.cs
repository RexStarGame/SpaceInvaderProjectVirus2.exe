using UnityEngine;

public class ShipMovement2Boss : MonoBehaviour
{
    public float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move the boss horizontally
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WallBoundry")
        {
            // If colliding with a wall, reverse the horizontal movement
            moveSpeed *= -1;

            // Ensure the ship stays on the same y-axis position
            float newY = collision.transform.position.y;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}