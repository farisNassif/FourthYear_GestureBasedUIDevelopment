using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for the Bullet prefab
// Reference : https://www.youtube.com/watch?v=wkKsl1Mfp5M&t=211s

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        // speed of bullet
        rb.velocity = transform.right * speed;
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Asteroid enemy = hitInfo.GetComponent<Asteroid>();
        if(enemy != null)
        {
            // destroy bullet and asteroid on collision
            Destroy(gameObject);
            Destroy(enemy.gameObject);
        }
        

    }
}
