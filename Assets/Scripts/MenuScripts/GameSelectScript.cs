using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Windows.Kinect;

// Script for loading into game one

public class GameSelectScript : MonoBehaviour
{
<<<<<<< HEAD
=======
    public TimeManager soundManager;
    
    // select game 
>>>>>>> cc231de811d55858918591c95213e5523bf35edc
    public void SelectGame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    // return to menu scene (no button)
    public void Return()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // restart game and return music to true (yes button)
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;

        // reset music 
        TimeManager.playedSound = true;
        TimeManager.playedMusic = true;
    }
}
