using UnityEngine;

public class EnemyWallBoundry2 : MonoBehaviour
{
    // Add the tag of the wall boundary in the Unity Editor
    public string wallBoundaryTag = "WallBoundry2";

    // OnTriggerEnter2D is called when a Collider2D other enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(wallBoundaryTag))
        {
            Destroy(gameObject); // Destroy the GameObject with this script
        }
    }
}
