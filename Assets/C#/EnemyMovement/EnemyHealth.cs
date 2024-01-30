using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    
    public static float health = 2;


    // Start is called before the first frame update
    void Awake()
    {
        health = 2; 
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
}
