using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /* Singleton so it can be accessed from other scripts */
    public static ScoreManager instance;
    /* Variables for keeping track of scores */
    public int score, highScore;
    /* Text display for in game purposes */
    public Text scoreText;
    
    /* Singleton Instance */
    private void Awake()
    {
        instance = this;
    }

    /* Increments current players score */
    public void AddScore()
    {
        score++;

        UpdateHighScore();

        scoreText.text = "Score: [" + score.ToString() + "]";
        // gameOverScoreText.text = score.ToString();
    }

    /* Basically if current score is greater than current high score, score = new high score */
    public void UpdateHighScore() 
    {
        if (score > highScore)
        {
            highScore = score;
           // highScoreText.text = highScore.ToString();
        }
    }

    /* So that local score doesn't persist after win or lose condition */
    public void ResetScore()
    {
        score = 0;
    }
}
