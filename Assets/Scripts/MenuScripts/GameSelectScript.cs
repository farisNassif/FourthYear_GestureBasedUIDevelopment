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

    /* If a swipe is detected go back in the menu */
    public static bool recentlySwiped = false;

    void Update() 
    {
        /* Swipe was detected .. */
        if (GameSelectScript.recentlySwiped == true) {
            /* Go back in the menu */
            Back();
            Debug.Log("Swipe, go back!");
            /* Make this false so it can be called again */
            GameSelectScript.recentlySwiped = false;
        }
    }

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
