using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Asteroid enemy object class */
public class Asteroid : MonoBehaviour {
    /* Speed changeable in inspector */
    public float speed = 35.0f;
    /* Asteroid rigidbody */
    private Rigidbody2D rb;
    /* Screen bounds, so it spawns within a certain range */
    private Vector2 screenBounds;


    /* Used for initialization */
    void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*2, Screen.height, Camera.main.transform.position.z));
    }

    /* If it goes out of bounds destroy it */
    void Update () {
        if(transform.position.x < screenBounds.x){
            Destroy(this.gameObject);
        }
    }
}