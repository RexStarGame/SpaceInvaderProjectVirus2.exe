using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;
    public float hInpudt;
    public static float damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hInpudt = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector2.right * hInpudt * moveSpeed * Time.deltaTime);
    }
    public void MoveSpeedChances(float chanceMoveSpeed)
    {
        moveSpeed += chanceMoveSpeed;
    }
    public void SetDamage(float newDamage)
    {
        damage += newDamage;
        damage += newDamage;
    }
}
