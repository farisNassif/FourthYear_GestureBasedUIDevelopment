using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Windows.Kinect;

/* Script for loading into games */
public class GameSelectScript : MonoBehaviour
{
    /* All the menu objects that need to be interacted with */
    public GameObject gameMenu;
    public GameObject mainMenu;
    public GameObject helpOne;
    public GameObject helpTwo;

    /* If a swipe is detected go back in the menu */
    public static bool recentlySwiped = false;

    void Update() 
    {
        /* Swipe was detected .. 
        ** See ImportGestureDatabase.cs for gesture importing & handling */
        if (GameSelectScript.recentlySwiped == true) {
            /* If the game menu is the active game object .. */
            if (gameMenu.activeSelf)
            {
                /* Go back in the menu */
                Back();
            } 
            /* If the user is on the help page for the first game .. */
            else if (helpOne.activeSelf)
            {
                /* Back to the game menu */
                BackHelpOne();
            }
            /* If the user is on the help page for the second game .. */
            else if (helpTwo.activeSelf)
            {
                /* Back to the game menu */
                BackHelpTwo();
            }
            /* Make this false so it can be called again 
            ** Cooldown period of 3 seconds - See ImportGestureDatabse.cs 
            ** This may be unnecessary due to the enumerator in the above script that controls the bool, here just incase! */
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

        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;

        TimeManager.playedSound = false;
        TimeManager.playedMusic = false;
        SoundManagerScript.GameMusic_1();
        
    }

    /* Play bird game */
    public void GameTwo()
    {
        SceneManager.LoadScene("GameTwo");
        Time.timeScale = 1f;
    }

    /* Back to the main menu */
    public void Back() 
    {
        gameMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    /* If the user is in game one's help menu, go back */
    public void BackHelpOne()
    {
        gameMenu.SetActive(true);
        helpOne.SetActive(false);
    }

    /* If the user is in game two's help menu, go back */
    public void BackHelpTwo()
    {
        gameMenu.SetActive(true);
        helpTwo.SetActive(false);
    }

    /* If the user wants to go to the instructions page for game one */
    public void ToHelpOne()
    {
        helpOne.SetActive(true);
        gameMenu.SetActive(false);
    }

    /* If the user wants to go to the instructions page for game two */
    public void ToHelpTwo()
    {
        helpTwo.SetActive(true);
        gameMenu.SetActive(false);
    }
}
