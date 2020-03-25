using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    /* Variables for keeping track of scores */
    public int score, highScore;
    /* Score values for collisions */
    private int goodBaloon = 1;
    private int badBaloon = 3;
    private int rareBaloon = 10;
    /* Text display for in game purposes */
    public Text scoreText;
    public Text highScoreText;
    /* If the current player set a new high score */
    public bool setNewHighScore = false;

    /* Singleton Instance */
    private void Awake()
    {
        instance = this;
    }

    void Start() {
        /* Get the previously stored high score and show it */
        GetHighScore();
    }

    /* Increments current players score */
    public void UpdateScore(int CollisionType)
    {
        /* If the player collides with a green balooon, execute this */
        if(CollisionType == 0) 
        {
            /* Increment Score by 1 */
            score = score + goodBaloon;
        } 
        /* If the player collides with a bad balooon, execute this */
        else if (CollisionType == 1)
        {
            /* Decrement Score by 3 */
            score -= badBaloon; 

            /* Ensuring the score won't ever go below 0 */
            if (score <= 0)
            {
                score = 0;
            }
        } 
        /* If the player collides with a rare balooon, execute this */
        else 
        {
            /* Increment Score by 5 */
            score += rareBaloon; 
        }

        /* Check if current score is better than previous high */
        UpdateHighScore(); 

        /* Set the text fields to reflect scores */
        highScoreText.text = "High Score: [" + highScore.ToString() + "]";
        scoreText.text = "Score: [" + score.ToString() + "]";
    }

    /* Basically if current score is greater than current high score, score = new high score */
    public void UpdateHighScore() 
    {
        if (score > highScore)
        {
            /* New high score set */
            highScore = score + 1;
            /* Display to the user the new high score */
            highScoreText.text = "High Score: [" + highScore.ToString() + "]";
            /* Save score locally so it persists */
            SetHighScore();
            /* Player set a new High Score! */
            setNewHighScore = true;
        }
    }

    /* Set score locally so it persists */
    public void SetHighScore()
    {
        PlayerPrefs.SetInt("balloonHS", score); 
    }

    /* On start set the high score to what was saved locally */
    public void GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("balloonHS");
    }

    /* So that local score doesn't persist after win or lose condition */
    public void ResetScore()
    {
        score = 0;
    }
}
