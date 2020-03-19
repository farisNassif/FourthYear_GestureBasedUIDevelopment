using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference : https://www.youtube.com/watch?v=HpJMhNmpIxY
// Script for temporary player movement (kinect functionality to be added)

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private float horiSpeed = 5f;
    private float vertSpeed = 1.5f;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        HandleMovement(horizontal, vertical);      
        
    }

    private void HandleMovement(float horizontal, float vertical)
    {
        myRigidbody.velocity = new Vector2(vertical, myRigidbody.velocity.y) * vertSpeed;
        myRigidbody.velocity = new Vector2(horizontal, myRigidbody.velocity.x) * horiSpeed;

    }

}
