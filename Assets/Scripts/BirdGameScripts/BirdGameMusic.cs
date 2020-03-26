using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that pretty much just handles and loops the music for the bird game */
public class BirdGameMusic : MonoBehaviour
{
    /* Start will always be 0 seconds so it can be compared against startingTime */
    float currentTime = 0f;
    /* Duration of music clip */
    float startingTime = 37f; 
    public static bool playedMusic = false;

    /* Executed on start */
    void Start()
    {
        playedMusic = false;
        currentTime = startingTime;
    }

    /* Update is called each frame */
    void Update()
    {
        PlayMusic();
        currentTime -= 1 * Time.deltaTime;

        /* If music ends, replay it */
        if (currentTime.ToString("0") == "0")
        {
            playedMusic = false;
            currentTime = startingTime; // Reset back to start
        }
    }

    /* Method that handles playing the backgroud music */
    public static void PlayMusic()
    {
        /* Bool check to make sure this doesn't execute more than once to not deafen the user */
        if (playedMusic == false)
        {
            SoundManagerScript.BirdGameMusicPlay();
            playedMusic = true;
        }
    }
}
