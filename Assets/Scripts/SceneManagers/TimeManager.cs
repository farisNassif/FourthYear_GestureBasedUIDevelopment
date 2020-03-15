using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Class that manages the timer for the Baloon bursting game, also handles certain sounds */
public class TimeManager : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 30f;
    public static bool playedMusic = false;
    public static bool playedSound = false;
    public Text countdownTimer; 
     
    /* When the game begins, set the Current time to 30 */
    void Start()
    {
        currentTime = startingTime;
    }

    /* Update is called once per frame */
    void Update()
    {
        PlayMusic();
        
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
            CountdownSound();
        }

        /* Times up! Game over */
        if (currentTime <= 0.5)
        {
            /* In future call a game over method that deals with UI stuff */
            Debug.Log("Game over");
            Time.timeScale = 0f; // Freeze the scene
            // TODO => GameOver();
        }

        /* Rounds it to a whole number */
        countdownTimer.text = currentTime.ToString("0");
    }

    /* Method that handles playing the backgroud music */
    public static void PlayMusic() {
        /* Bool check to make sure this doesn't execute more than once to not deafen the user */
        if (playedMusic == false) {
            SoundManagerScript.GameMusic_1();
            playedMusic = true;
        }
    }

    /* Method that handles playing the countdown sound */
    public static void CountdownSound() {
        /* Bool check to make sure this doesn't execute more than once to not deafen the user */
        if (playedSound == false) {
            SoundManagerScript.CountdownClip();
            playedSound = true;
        }
    }
}
