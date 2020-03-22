using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that pretty much just handles and loops the music for the bird game */
public class BirdGameMusic : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 37f; // Duration of music clip
    public static bool playedMusic = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
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
