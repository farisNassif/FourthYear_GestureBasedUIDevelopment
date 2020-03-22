using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public Transform bullet;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // shoot function for keyboard
            Shoot();
            Debug.Log("Shoot");
        }
    }

    void Shoot()
    {
        // create bullet prefab
        Instantiate(bulletPrefab, bullet.position, bullet.rotation);
    }
}
