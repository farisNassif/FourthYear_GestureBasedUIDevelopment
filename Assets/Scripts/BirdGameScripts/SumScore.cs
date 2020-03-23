using UnityEngine;

/* Class that manages score for the bird game */
public class SumScore {

    public static int Score { get; protected set; }
    public static int HighScore { get; set; }

    /* Easy reference to manager instance */
    private static SumScoreManager mgr; 

    /* Private constructor to ensure only one copy exists */
    private SumScore () { }
	
    /* Add points to score */
    public static void Add (int pointsToAdd) {
        // Debug.Log(pointsToAdd + " points " + ((pointsToAdd > 0) ? "added" : "removed"));
        /* Add points to current score */
        Score += pointsToAdd; 
        if (MgrSet()) {
            /* Make sure doesn't go negative */
            if (Score < 0 && !mgr.allowNegative)
                Score = 0; 
            /* Let the manager know score was changed */
            mgr.Updated(); 
        }
    }

    /* Removes (x) points from total score */
    public static void Subtract (int pointsToSubtract) {
        Add(-pointsToSubtract);
    }

    /* Sets Score to 0 and updates manager */
    public static void Reset () {
        Debug.Log("Reset score");
        Score = 0;
        if(MgrSet()) {
            mgr.Updated();
        }
    }

    /* Checks and sets references needed for the script 
    ** True if successful, false if failed */
    static bool MgrSet () {
        if (mgr == null) {
            /* Set instance reference */
            mgr = SumScoreManager.instance; 
            if (mgr == null) {
                /* Throw error message if we can't link */
                Debug.LogError("<b>SumScoreManager.instance</b> cannot be found. Make sure object is active in inspector.");
                return false;
            }
        }
        return true;
    }

    /* Checks score against high score and saves if higher */
    public static void SaveHighScore () {
        if (Score > HighScore) {
            // Debug.Log("New high score " + Score);
            HighScore = Score;
            /* Store high score in player prefs 
            ** https://docs.unity3d.com/ScriptReference/PlayerPrefs.html */
            PlayerPrefs.SetInt("sumHS", Score); 
            if (MgrSet())
                /* Notify manager of change */
                mgr.UpdatedHS(); 
        }
    }

    /* Reset high score and clear from player prefs */
    public static void ClearHighScore () {
        // Debug.Log("Deleting high score");
        PlayerPrefs.DeleteKey("sumHS");
        HighScore = 0;
        if (MgrSet())
            /* Notify manager of change */
            mgr.UpdatedHS(); 
    }
}
