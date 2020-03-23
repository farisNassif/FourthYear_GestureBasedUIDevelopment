using UnityEngine;

/* Score class that manages the Score of the bird game, kinda like the runner */
public class BirdScoreManager : MonoBehaviour {

    void Update () {
        /* Use Time.deltaTime to create a steady addition of points.
        ** This would add 100 points per second */
        SumScore.Add(Mathf.RoundToInt(Time.deltaTime * 100));

        /* Keep checking to see if new high score was beaten */
        CheckHighScore();
    }

    /* Add (x) points */
	public void AddPoints(int points) {
        SumScore.Add(points);
    }

    /* Subtract (x) points */
    public void SubtractPoints (int points) {
        SumScore.Add(-points);
    }

    /* Reset score to 0 */
    public void ResetPoints () {
        SumScore.Reset();
    }

    /* Check to see if high score was beaten */
    public void CheckHighScore () {
        if (SumScore.Score > SumScore.HighScore)
            SumScore.SaveHighScore();
    }


    /* Clear the high score */
    public void ClearHighScore () {
        SumScore.ClearHighScore();
    }
}
