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
            if (other.transform != transform && other.transform.IsChildOf(transform))
            {
                // Ignore collisions with child objects
                return;
            }

            Destroy(gameObject); // Destroy the parent GameObject along with its children
        }
    }
}
