using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Class that manages the bird shooting */
public class ShootScript : MonoBehaviour
{
    public Transform bullet;
    public GameObject bulletPrefab;
    
    [HideInInspector]

    /* Player starts with full charge */
    public static double currentCharge = 100;
    /* Should be displaying the power bar or percentage, 100% should be required to shoot */
    public Text shootCharge;
    void Update()
    {
        /* Max the charge out at 100 */
        if (currentCharge != 100)
        {
            currentCharge = currentCharge + 0.7;
        }

        /* Display charge in real time */
        shootCharge.text = "Shoot charge: " + currentCharge.ToString("0") + "%";

        if (Input.GetKeyDown(KeyCode.Space) && currentCharge == 100)
        {
            // shoot function for keyboard
            Shoot();
            currentCharge = 0;
            Debug.Log("Shoot");
        }
    }

    void Shoot()
    {
        // create bullet prefab
        Instantiate(bulletPrefab, bullet.position, bullet.rotation);
    }
}
