using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Reference : https://www.youtube.com/watch?v=HpJMhNmpIxY */
/* Script for temporary player movement (kinect functionality to be added) */

public class PlayerMovement : MonoBehaviour
{
    /* Bird body */
    private Rigidbody2D myRigidbody;
    /* Speed variables for flying */
    private float horiSpeed = 5f;
    private float vertSpeed = 0.7f;

    /* Text display for in game purposes, changes depending on gesture alerting the player */
    public Text statusOfFlying;

    [HideInInspector]
    /* Vars to keep track of the last action executed by the player/bird */
    public static bool flyingUp = false;
    public static bool flyingDown = false;
    public static bool hover = false;

    void Start()
    {
        /* On start, init the rigid body */
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /* Execute the Coroutine every iteration */
        StartCoroutine(Fly());
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
        double X = 0.5;
        double timePassed = 0;

        /* Execute the while for x seconds */
        while (timePassed < X)
        {
            if(PlayerMovement.flyingUp == true)
            {
                /* Output message for the user of the birds status */
                statusOfFlying.text = "Flying Status: [Ascending]";
                /* Move the bird up */
                HandleMovement(0f, 1);   
                
            } 
            if(PlayerMovement.flyingDown == true)
            {
                /* Output message for the user of the birds status */
                statusOfFlying.text = "Flying Status: [Descending]";
                /* Move the bird down */
                HandleMovement(0f, -1);   
            }
            if(PlayerMovement.hover == true)
            {
                /* Output message for the user of the birds status */
                statusOfFlying.text = "Flying Status: [Hovering]";
                /* Move the bird down */
                HandleMovement(0f, 0);
            }

            /* Calculate real time since while loop was executed */
            timePassed += Time.deltaTime;
    
            /* When the while makes its last iteration, set flying to false, user has to flap to make this execute again */
            yield return PlayerMovement.flyingDown = false && PlayerMovement.flyingUp == false && PlayerMovement.hover == true;
        }  
    }
}
