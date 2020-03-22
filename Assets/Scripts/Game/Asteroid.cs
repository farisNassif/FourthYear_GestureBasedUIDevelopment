using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    public float speed = 35.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;


    // Use this for initialization
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