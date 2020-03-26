using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* This Script is for managing the main menu upon loading the application
** References : https://www.youtube.com/watch?v=zc8ac_qUXQY&t=583s (Brackeys tutorial on scene management)
** https://answers.unity.com/questions/1072521/set-gui-text-activeinactive-by-clicking-on-a-butto-1.html (Set GUI element to true or false with booleans) */
public class MainMenuScript : MonoBehaviour
{
    /* Menu objects required for navigation */
    public GameObject gameMenu;
    public GameObject mainMenu;
    public GameObject scoresMenu;

    /* Bool to control swipe navigation, 3 second swipe cooldown
    ** See ImportGestureDatabase.cs for more*/
    public static bool recentlySwiped = false;

    void Update()
    {
        if (MainMenuScript.recentlySwiped == true) {
            Debug.Log("Swiped");
            MainMenuScript.recentlySwiped = false;
        }
    }

    /* Select Game function */
    public void SelectGame()
    {
        /* Loads the game menu and makes the initial menu inactive */
        gameMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    /* Current high scores for both games */
    public void HighScores()
    {
        scoresMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
