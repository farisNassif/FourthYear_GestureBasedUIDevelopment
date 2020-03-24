using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Windows.Kinect;

/* Script for loading into games */
public class GameSelectScript : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject mainMenu;
    TimeManager soundManager;

    /* Select game */
    public void SelectGame()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        TimeManager.playedSound = false;
        TimeManager.playedMusic = false;
    }

    /* Return to menu scene */ 
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

    /* Play bird game */
    public void GameTwo()
    {
        SceneManager.LoadScene("GameTwo");
        Time.timeScale = 1f;
    }

    public void Back() 
    {
        gameMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
