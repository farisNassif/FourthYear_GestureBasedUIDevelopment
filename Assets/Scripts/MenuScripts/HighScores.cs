using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Script thats responsible for showing the high scores for both games */
public class HighScores : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject scoresMenu;
    public Text BaloonScore;
    public Text BirdScore;

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
    void Start()
    {
        mainMenu.SetActive(false);
        BaloonScore.text = "Baloon Game - High Score: [TODO]";
        BirdScore.text = "Bird Game - High Score: " + SumScore.HighScore;
    }

    public void Back()
    {
        scoresMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ClearBirdScore()
    {
        SumScore.ClearHighScore();   
        BirdScore.text = "Bird Game - High Score: " + SumScore.HighScore;
    }

}
