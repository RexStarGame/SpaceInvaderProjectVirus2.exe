using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Define the boundaries of your game world
    public float WorldMinX = -10f;
    public float WorldMaxX = 10f;
    public float WorldMinY = -10f;
    public float WorldMaxY = 10f;
    public float WorldMinZ = -10f;
    public float WorldMaxZ = 10f;

    // Singleton pattern to access the GameManager instance from other scripts
    public static GameManager Instance;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}