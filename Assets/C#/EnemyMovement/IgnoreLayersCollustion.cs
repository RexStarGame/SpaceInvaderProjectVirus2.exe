using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public string ignoreLayer = "EnemyShip";

    void Start()
    {
        // Ignore collisions with the same layer
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(ignoreLayer), LayerMask.NameToLayer(ignoreLayer));
    }
}

