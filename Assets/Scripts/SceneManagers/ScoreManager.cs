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
    /* Score values for collisions */
    private int goodBaloon = 1;
    private int badBaloon = 3;
    private int rareBaloon = 10;
    /* Text display for in game purposes */
    public Text scoreText;
    
    /* Singleton Instance */
    private void Awake()
    {
        instance = this;
    }

    /* Increments current players score */
    public void UpdateScore(int CollisionType)
    {
        UpdateHighScore(); // Check if current score is better than previous high (NOT IMPLEMENTED YET)

        /* If the player collides with a green balooon, execute this */
        if(CollisionType == 0) 
        {
            // Increment Score by 1
            score = score + goodBaloon; 
        } 
        /* If the player collides with a bad balooon, execute this */
        else if (CollisionType == 1)
        {
            // Decrement Score by 3
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
            // Increment Score by 5
            score += rareBaloon; 
        }

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
