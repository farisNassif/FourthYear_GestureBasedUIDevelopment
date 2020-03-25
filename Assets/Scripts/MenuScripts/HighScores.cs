using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Script thats responsible for showing the high scores for both games */
public class HighScores : MonoBehaviour
{
    /* Menu Objects */
    public GameObject mainMenu;
    public GameObject scoresMenu;
    /* Textbox objects */
    public Text BaloonScore;
    public Text BirdScore;

    /* Need to manually get the balloon high score because of the way the script was written */
    public int balloonHighScore;
    
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

    /* Called on startup */
    void Start()
    {
        /* Make the previous menu inactive */
        mainMenu.SetActive(false);
        /* Need to get this manually unlike the bird game score */
        GetBalloonHighScore();
        /* Set the text fields based on stored high scores (in playerprefs) */
        BaloonScore.text = "Balloon Game - High Score: " + balloonHighScore;
        BirdScore.text = "Bird Game - High Score: " + SumScore.HighScore;
    }

    /* Back button logic */
    public void Back()
    {
        scoresMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    /* On start set the high score to what was saved locally */
    public void GetBalloonHighScore()
    {
        balloonHighScore = PlayerPrefs.GetInt("balloonHS");
    }

    /* Clear bird game score logic */
    public void ClearBirdScore()
    {
        SumScore.ClearHighScore();   
        /* Once it was cleared show the new score (Should always be 0 after resetting) */
        BirdScore.text = "Bird Game - High Score: " + SumScore.HighScore;
    }

    /* Clear balloon score logic */
    public void ClearBalloonScore()
    {
        PlayerPrefs.DeleteKey("balloonHS");
        /* Once it was cleared show the new score (Should always be 0 after resetting) */
        BaloonScore.text = "Balloon Game - High Score: 0";
    }
}
