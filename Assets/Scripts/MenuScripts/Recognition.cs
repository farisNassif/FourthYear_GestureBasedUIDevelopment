using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script used for voice recognition
// Reference : https://www.youtube.com/watch?v=HwT6QyOA80E
public class Recognition : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer;

    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public GameObject gameMenu;
    public GameObject mainMenu; 
    public GameObject scoreMenu;

    public Text BirdScore;

    // Add any extra words here for actions
    void Start()
    {
        gameMenu.SetActive(false);
        mainMenu.SetActive(true);

        // Add select to dictionary
        keywords.Add("select", () =>
        {
            SelectCalled();
        });

        // Add one to dictionary
        keywords.Add("one", () =>
        {
            OneCalled();
        });

        // Add two to dictionary
        keywords.Add("two", () =>
        {
            TwoCalled();
        });

        // Add back to dictionary
        keywords.Add("back", () =>
        {
            BackCalled();
        });

        keywords.Add("score", () =>
        {
            ScoreCalled();
        });

        keywords.Add("clear", () =>
        {
            ClearCalled();
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeyWordRecognizerOnPhraseRecognized;
        keywordRecognizer.Start();
    }

    // if keyword is in the list of recognized words
    void KeyWordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }


    void SelectCalled()
    {
        // saying select will deactivate main menu and bring player to the game select menu
        gameMenu.SetActive(true);
        mainMenu.SetActive(false);

        // debug log for confirmation
        if (gameMenu.activeSelf)
        {
            Debug.Log("You just said Go");
        }
        else
        {
            Debug.Log("Not active");
        }
    }


    void OneCalled()
    {
        // this will load into the scene one if "one" or "game one" is said (both are recognised)
        SceneManager.LoadScene("MainScene");
    }

    void TwoCalled()
    {
        // this will load into the scene one if "one" or "game one" is said (both are recognised)
        SceneManager.LoadScene("GameTwo");
    }

    void BackCalled()
    {
        // this will go back to main menu if in game select menu
        gameMenu.SetActive(false);
        mainMenu.SetActive(true);
        scoreMenu.SetActive(false);

    }

    void ScoreCalled()
    {
        scoreMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    void ClearCalled()
    {
        SumScore.ClearHighScore();
        BirdScore.text = "Bird Game - High Score: " + SumScore.HighScore;
    }

}