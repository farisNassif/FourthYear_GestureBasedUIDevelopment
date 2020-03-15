using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Class that manages the timer for the Baloon bursting game */
public class TimeManager : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 30f;
    bool playedSound = false;
    public Text countdownTimer; 
     
    /* When the game begins, set the Current time to 30 */
    void Start()
    {
        currentTime = startingTime;
    }

    /* Update is called once per frame */
    void Update()
    {
        /* Decrement by 1 second per second */
        currentTime -= 1 * Time.deltaTime;

        /* Change the colour of the timer to yellow when theres less than 20 seconds left */
        if (currentTime >=10 && currentTime < 20)
        {
            countdownTimer.color = Color.yellow;
        }

        /* Change the colour of the timer to red when theres less than 10 seconds left */
        if (currentTime < 10)
        {
            countdownTimer.color = Color.red;
        }

        /* Start a countdown when 7 seconds remain */
        if (currentTime.ToString("0") == "7")
        {
            if(playedSound == false)
            {
                SoundManagerScript.CountdownClip();
                playedSound = true; // To make sure this doesn't execute multiple times to not deafen the user
            }    
        }

        /* Times up! Game over */
        if (currentTime <= 0.5)
        {
            Debug.Log("Game over");
            // TODO => GameOver();
        }

        /* Rounds it to a whole number */
        countdownTimer.text = currentTime.ToString("0");
    }
}
