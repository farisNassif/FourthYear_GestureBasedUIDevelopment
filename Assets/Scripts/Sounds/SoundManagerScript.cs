using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
* All sounds used within the project should be declared in this script.
* Most of the sounds were retrieved from => https://freesound.org/
*/
public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip CountdownSound, GameMusic, Pop1, Pop2, Pop3, Flap;
    static AudioSource AudioSrc;
    private float soundVolume = 1f;
    public static System.Random r = new System.Random();

    /* Start is called before the first frame update */
    void Start()
    {
        /* All references for the sound files */
        GameMusic = Resources.Load<AudioClip>("GameMusic1");
        CountdownSound = Resources.Load<AudioClip>("Countdown");
        Pop1 = Resources.Load<AudioClip>("Pop1");
        Pop2 = Resources.Load<AudioClip>("Pop2");
        Pop3 = Resources.Load<AudioClip>("Pop3");
        Flap = Resources.Load<AudioClip>("Flap");

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

    /* Flap sound for when a flapping user is picked up */
    public static void FlapClip()
    {
        AudioSrc.PlayOneShot(Flap);
    }

    /* Plays a random Pop */
    public static void PopClip()
    {
        /* Generates either 0, 1, 2 */
        RandomGenerator random = new RandomGenerator(0,3); 

        if (random.randomNumber == 0)
        {
            AudioSrc.PlayOneShot(Pop1);
        } 
        else if (random.randomNumber == 1) 
        {
            AudioSrc.PlayOneShot(Pop2);
        } 
        else 
        {
            AudioSrc.PlayOneShot(Pop3);
        }
    }

    public static void Stop()
    {
        AudioSrc.Stop();
    }

}
