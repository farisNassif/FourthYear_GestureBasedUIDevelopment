using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Script used for voice recognition in Menus
** Reference : https://www.youtube.com/watch?v=HwT6QyOA80E */
public class Recognition : MonoBehaviour
{
    /* KeywordRecognizer object initializer */
    KeywordRecognizer keywordRecognizer;

    /* Dictionary containing all defined keywords for Game Two */
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    /* List of Gameobjects defining Menus and Scenes to navigate to from the Menu */
    public GameObject gameMenu;
    public GameObject mainMenu; 
    public GameObject scoreMenu;
    public GameObject helpOne;
    public GameObject helpTwo;

    /* Scores for the score page */
    public Text BirdScore;
    public Text BaloonScore;

    /* Called once */
    void Start()
    {
        /* Boolean controlled menu activity */
        gameMenu.SetActive(false);
        mainMenu.SetActive(true);

        /* Add 'select' keyword to the keywords dictionary */
        keywords.Add("select", () =>
        {
            /* Fire off the method */
            SelectCalled();
        });

        /* Add 'quit' keyword to the keywords dictionary */
        keywords.Add("quit", () =>
        {
            /* Fire off the method */
            QuitCalled();
        });

        /* Add 'one' keyword to the keywords dictionary */
        keywords.Add("one", () =>
        {
            /* Fire off the method */
            OneCalled();
        });

        /* Add 'two' keyword to the keywords dictionary */
        keywords.Add("two", () =>
        {
            /* Fire off the method */
            TwoCalled();
        });

        /* Add 'back' keyword to the keywords dictionary */
        keywords.Add("back", () =>
        {
            /* Fire off the method */
            BackCalled();
        });

        /* Add 'score' keyword to the keywords dictionary */
        keywords.Add("score", () =>
        {
            /* Fire off the method */
            ScoreCalled();
        });

        /* Add 'bird' keyword to the keywords dictionary */
        keywords.Add("bird", () =>
        {
            /* Fire off the method */
            BirdCalled();
        });

        /* Add 'balloon' keyword to the keywords dictionary */
        keywords.Add("balloon", () =>
        {
            /* Fire off the method */
            BalloonCalled();
        });

        /* Add 'help' keyword to the keywords dictionary */
        keywords.Add("help one", () =>
        {
            /* Fire off the method */
            BalloonHelpCalled();
        });

        /* Add 'assist' keyword to the keywords dictionary */
        keywords.Add("help two", () =>
        {
            /* Fire off the method */
            BirdHelpCalled();
        });

        /* New KeywordRecognizer object and passing all the keys (words) */
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        /* When a phrase was recognized, pop off a new keywordRecognizer */
        keywordRecognizer.OnPhraseRecognized += KeyWordRecognizerOnPhraseRecognized;
        /* Start figuring out what to do once it was recognized */
        keywordRecognizer.Start();
    }

    /* If keyword is in the list of recognized words */
    void KeyWordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    /* When 'Select' was picked up */
    void SelectCalled()
    {
        /* Deactivate main menu and bring player to the game select menu */
        gameMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    /* When 'One' was picked up */
    void QuitCalled() 
    {
        /* Terminate the Application */
        Application.Quit();
    }

    /* When 'One' was picked up */
    void OneCalled()
    {
        /* Will load into the scene one if "one" or "game one" is said (both are recognised) */
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    void TwoCalled()
    {
        /* Will load into the scene one if "one" or "game one" is said (both are recognised) */
        SceneManager.LoadScene("GameTwo");
        Time.timeScale = 1f;
    }

    /* When 'Back' was picked up */
    void BackCalled()
    {
        /* Will go back to main menu if in game select menu */
        gameMenu.SetActive(false);
        mainMenu.SetActive(true);
        scoreMenu.SetActive(false);
        helpOne.SetActive(false);
        helpTwo.SetActive(false);
    }

    /* When 'Score' was picked up */
    void ScoreCalled()
    {
        /* Go to the Score page to see high scores */
        scoreMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    /* When 'Bird' was picked up */
    void BirdCalled()
    {
        /* Delete the local storage of the Bird high scores */
        SumScore.ClearHighScore();
        BirdScore.text = "Game Two - High Score: " + SumScore.HighScore;
    }

    /* When 'Balloon' was picked up */
    void BalloonCalled()
    {
        /* Delete the local storage of the Balloon high scores */
        PlayerPrefs.DeleteKey("balloonHS");
        BaloonScore.text = "Game One - High Score: 0";
    }

    /* When 'Help' was picked up */
    void BalloonHelpCalled()
    {
        /* Go to the Balloon Game tutorial help page */
        helpOne.SetActive(true);
        gameMenu.SetActive(false);
        
    }

    /* When 'Assist' was picked up */
    void BirdHelpCalled()
    {
        /* Go to the Bird Game tutorial help page */
        helpTwo.SetActive(true);
        gameMenu.SetActive(false);
    }
}