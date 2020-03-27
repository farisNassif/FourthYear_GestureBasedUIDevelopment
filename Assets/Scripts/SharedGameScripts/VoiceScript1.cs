using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;

// Script used for voice recognition in game two
// Reference : https://www.youtube.com/watch?v=HwT6QyOA80E
public class VoiceScript1 : MonoBehaviour
{


    KeywordRecognizer keywordRecognizer;

    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Add any extra words here for actions
    void Start()
    {
        
        // Add fire to dictionary and call function
        keywords.Add("no", () =>
        {
            NoCalled();
        });

        keywords.Add("yes", () =>
        {
            YesCalled();
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

    void NoCalled()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void YesCalled()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

}