﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Reference : https://www.youtube.com/watch?v=HpJMhNmpIxY */
/* Script for temporary player movement (kinect functionality to be added) */

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    /* Bird body */
    private Rigidbody2D myRigidbody;
    /* Speed variables for flying */
    private float horiSpeed = 5f;
    private float vertSpeed = 1f;

    /* After respawn become invunerable for x seconds */
    private bool invincible = false;

    /* Text display for in game purposes, changes depending on gesture alerting the player */
    public Text statusOfFlying;

    [HideInInspector]
    /* Vars to keep track of the last action executed by the player/bird */
    public static bool flyingUp = false;
    public static bool flyingDown = false;
    public static bool hover = false;

    public GameObject gameOver;

    /* Text the player sees when they finish the level */
    public Text gameOverScore;

    void Start()
    {
        /* On start, init the rigid body */
        myRigidbody = GetComponent<Rigidbody2D>();

        // disable game over menu
        gameOver.SetActive(false);
    }

    void Update()
    {
        /* Temp for testing, using kinect on deployment */
        if (Input.GetKeyDown("a"))
        {
            PlayerMovement.flyingUp = true;
            PlayerMovement.flyingDown = false;
            PlayerMovement.hover = false;
        }

        if (Input.GetKeyDown("s"))
        {
            PlayerMovement.hover = true;
            PlayerMovement.flyingUp = false;
            PlayerMovement.flyingDown = false;
        }

        if (Input.GetKeyDown("d"))
        {
            PlayerMovement.flyingDown = true;
            PlayerMovement.hover = false;
            PlayerMovement.flyingUp = false;
        }

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

    /* If the bird collides with something .. */
    void OnCollisionEnter2D(Collision2D col)
    {
        /* If it was an asteroid and player hasn't respawned recently .. */
        if (col.gameObject.name == "Asteroid 2(Clone)" && invincible == false)
        {
            Debug.Log("Die"); // Test
            SoundManagerScript.BirdDieClip(); // Play death sound

            /* Decrement a life visually */
            HealthBarHUDTester.Hurt(1f);

            /* No health left, game over! */
            if (PlayerStats.Instance.Health <= 0)
            {
                /* Stop sounds */
                SoundManagerScript.Stop();
                SoundManagerScript.BirdGameOverClip();

                /* */
                SetGameOverScore();
                /* Set the game over menu active */
                gameOver.SetActive(true);
                /* Freeze the scene */
                Time.timeScale = 0f;
            }

            /* Replace the player back at the starting point, illusion of respawning */
            transform.position = respawnPoint.transform.position;
            /* Become immune to damage for 3 seconds */     
            StartCoroutine(InvunerableTimer());  
            
        }
    }

    IEnumerator InvunerableTimer()
    {
        /* Invunerability for 3 seconds after dying */
        invincible = true; 
        yield return new WaitForSeconds(3);
        invincible = false;
    }

    /* Message to the user after they finish the level */
    public void SetGameOverScore() 
    {
        /* If they beat the old high score, make the message green */
        if (SumScore.Score >= SumScore.HighScore)
        {
            gameOverScore.text = "Score - " + SumScore.Score + "\n New High Score!";
            gameOverScore.color = Color.green;
        } 
        /* Otherwise change the message and make it red */
        else if (SumScore.Score < SumScore.HighScore)
        {
            gameOverScore.text = "Score - " + SumScore.Score + "\n No new High Score :(";
            gameOverScore.color = Color.red;
        }       
    }
}
