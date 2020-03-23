using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Windows.Kinect;

// Script for loading into game one

public class GameSelectScript : MonoBehaviour
{
    TimeManager soundManager;

    // select game 
    public void SelectGame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        TimeManager.playedSound = false;
        TimeManager.playedMusic = false;
    }

    // return to menu scene 
    public void Return()
    {
        SceneManager.LoadScene("MenuScene");
    }

    /* Restart game and return music to true (yes button) */
    public void RestartGame()
    {
        Debug.Log("Restart game");

        /* DON'T UNCOMMENT THIS IT WILL BREAK YOUR COMPUTER */
        //SceneManager.LoadScene("MainScene");
        //Time.timeScale = 1f;

        /*TimeManager.playedSound = false;
        TimeManager.playedMusic = false;
        SoundManagerScript.GameMusic_1();

        if (TimeManager.currentTime.ToString("0") == "7")
        {
            SoundManagerScript.CountdownClip();
        }
        */
    }

    public void GameTwo()
    {
        SceneManager.LoadScene("GameTwo");
        Time.timeScale = 1f;
    }

}
