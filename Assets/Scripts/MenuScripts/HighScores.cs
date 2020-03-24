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


    void Start()
    {
        mainMenu.SetActive(false);
        BaloonScore.text = "Baloon Game Score: [1 gorillion]";
        BirdScore.text = "Score: [" + SumScore.HighScore + "]";
    }

    public void Back()
    {
        scoresMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
