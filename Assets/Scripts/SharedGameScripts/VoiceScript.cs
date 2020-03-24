using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;

// Script used for voice recognition in game two
// Reference : https://www.youtube.com/watch?v=HwT6QyOA80E
public class VoiceScript : MonoBehaviour
{
    public Transform bullet;
    public GameObject bulletPrefab;

    KeywordRecognizer keywordRecognizer;

    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Add any extra words here for actions
    void Start()
    {
        
        // Add fire to dictionary and call function
        keywords.Add("fire", () =>
        {
            FireCalled();
        });

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

    // call fire
    void FireCalled()
    {
        if (ShootScript.currentCharge==100)
        {
            // create bullet if fire is said, rest is handled in bullet script
            Debug.Log("Fire called");
            Instantiate(bulletPrefab, bullet.position, bullet.rotation);
            ShootScript.currentCharge = 0;
        }

    }

    void NoCalled()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void YesCalled()
    {
        SceneManager.LoadScene("GameTwo");
        Time.timeScale = 1f;
    }

}