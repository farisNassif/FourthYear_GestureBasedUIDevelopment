using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip CountdownSound;
    static AudioSource AudioSrc;
    private float soundVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        CountdownSound = Resources.Load<AudioClip>("Countdown");

        AudioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        AudioSrc.volume = soundVolume;
    }

    public static void CountdownClip()
    {
        AudioSrc.PlayOneShot(CountdownSound);
    }

}
