using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Reference : https://www.youtube.com/watch?v=HpJMhNmpIxY */
/* Script for temporary player movement (kinect functionality to be added) */

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private bool m_isAxisInUse = false;
    private float horiSpeed = 5f;
    private float vertSpeed = 1.5f;
    public static bool IsFlying = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /* If there was a flying gesture picked up recently .. */
        if (PlayerMovement.IsFlying == true)
        {
            /* Play a flap sound */
            SoundManagerScript.FlapClip();
            /* @ Alex if you wanna test without the kinect you can use wsad normally if you wanna add anything in */


            Debug.Log("fly bird please");
            StartCoroutine(Fly());
        } else {
            // Todo -> Fall or something?
            HandleMovement(0f, 0f); 
        }
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            HandleMovement(horizontal, vertical);
    }

    /* Handles the flying movement of the bird */
    private void HandleMovement(float horizontal, float vertical)
    {
        myRigidbody.velocity = new Vector2(vertical, myRigidbody.velocity.y) * vertSpeed;
        myRigidbody.velocity = new Vector2(horizontal, myRigidbody.velocity.x) * horiSpeed;

    }

    /* Enumerator that makes the bird fly for X seconds after a flying gesture was caught.
    ** At the end of those X seconds flying is false, until another flying gesture was picked up
    */
    IEnumerator Fly()
    {
        float X = 2;
        float timePassed = 0;

        /* Execute the while for x seconds */
        while (timePassed < X)
        {
            /* Move the bird */
            HandleMovement(0f, 1f);   

            /* Calculate real time since while loop was executed */
            timePassed += Time.deltaTime;
    
            /* When the while makes its last iteration, set flying to false, user has to flap to make this execute again */
            yield return PlayerMovement.IsFlying = false;
        }  
    }
}
