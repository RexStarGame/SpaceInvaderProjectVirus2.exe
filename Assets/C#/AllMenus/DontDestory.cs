using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    private static GameObject[] persistenObject = new GameObject[3];
    public int objectIndext;
    // Start is called before the first frame update
    void Awake()
    {
        if (persistenObject[objectIndext] == null)
        {
            persistenObject[objectIndext] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistenObject[objectIndext] != gameObject)
        {
            Destroy(gameObject);
        }
        
    }

  
}
