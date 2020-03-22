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
            Debug.Log("her");
            /* Destroy bullet and asteroid on collision */
            Destroy(gameObject);
            Destroy(enemy.gameObject);
            /* If Player kills an asteroid while at full health, give them another empty heart and fill it by 25% */
            if (PlayerStats.Instance.Health == PlayerStats.Instance.MaxHealth)
            {
                /* Add another heart */
                HealthBarHUDTester.AddHealth();
                /* Empty the new heart */
                HealthBarHUDTester.Hurt(1f);
                /* Fill the heart up by 25% */
                HealthBarHUDTester.Heal(0.25f);
            } else  {
                /* Otherwise just heal them by 25% of a heart */
                HealthBarHUDTester.Heal(0.25f);
            }
        }
    }
}
