using UnityEngine;
using UnityEngine.UI;

/* Manager for SumScore accessible from inspector */
public class SumScoreManager : MonoBehaviour {

    /* Static instance for singleton */
    public static SumScoreManager instance = null;  

    public int initialScore = 0;
    public bool storeHighScore = true, allowNegative = true;

    /* Text field displaying current score */
    public Text field; 
    /* Text field displaying high score */
    public Text highScoreField; 

    void Awake() {
        /* Ensure only one instance is running */
        if (instance == null)
        /* Set instance to this object */
            instance = this; 
        else
            /* Seppuku */
            Destroy(gameObject); 
        /* Make sure the linked references didn't go missing */
        if (field == null)
            Debug.LogError("Missing reference to 'field' on <b>SumScoreManager</b> component");
        if (storeHighScore && highScoreField == null)
            Debug.LogError("Missing reference to 'highScoreField' on <b>SumScoreManager</b> component");
    }

    void Start() {
        /* Ensure score is 0 when object loads */
        SumScore.Reset(); 
        if (initialScore != 0)
            SumScore.Add(initialScore); 
        if (storeHighScore) {
            if (PlayerPrefs.HasKey("sumHS")) { 
                /* Set high score value and tell manager */
                SumScore.HighScore = PlayerPrefs.GetInt("sumHS");
                UpdatedHS();
            }
            else
                SumScore.HighScore = 0;
        }
        /* Set initial score in UI */
        Updated(); 
    }

    /* Notify this manager of a change in score */
    public void Updated () {
        /* Post new score to text field */
        field.text = SumScore.Score.ToString("0"); 
    }

    /* Notify this manager of a change in high score */
    public void UpdatedHS () {
        if(storeHighScore)
            /* Post new high score to text field */
            highScoreField.text = SumScore.HighScore.ToString("0"); 
    }
}
