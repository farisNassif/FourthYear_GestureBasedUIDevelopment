using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
* All sounds used within the project should be declared in this script.
* Most of the sounds were retrieved from => https://freesound.org/
*/
public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip CountdownSound, GameMusic;
    static AudioSource AudioSrc;
    private float soundVolume = 1f;

    /* Start is called before the first frame update */
    void Start()
    {
        GameMusic = Resources.Load<AudioClip>("GameMusic1");
        CountdownSound = Resources.Load<AudioClip>("Countdown");

        AudioSrc = GetComponent<AudioSource> ();
    }

    /* Update is called once per frame */
    void Update()
    {
        AudioSrc.volume = soundVolume;
    }

    /* Background game music for the first game */
    public static void GameMusic_1()
    {
        AudioSrc.PlayOneShot(GameMusic);
    }

    /* Clip that plays a tick tock sound when a timer is about to expire */
    public static void CountdownClip()
    {
        AudioSrc.PlayOneShot(CountdownSound);
    }

}
