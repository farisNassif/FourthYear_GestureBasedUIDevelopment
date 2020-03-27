using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;

/* Script used for voice recognition in Game One
** Reference : https://www.youtube.com/watch?v=HwT6QyOA80E */
public class VoiceScript : MonoBehaviour
{   
    /* Since we're calling 'Fire', need to create the bullet here on the fly */
    public Transform bullet;
    public GameObject bulletPrefab;
    /* KeywordRecognizer object initializer */
    KeywordRecognizer keywordRecognizer;

    /* Dictionary containing all defined keywords for Game Two */
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    void Start()
    {
        /* Add No to dictionary and call function */
        keywords.Add("no", () =>
        {
            /* When 'No' is picked up, fire off this method */
            NoCalled();
        });

        /* Add Yes to dictionary and call function */
        keywords.Add("yes", () =>
        {
            /* When 'Yes' is picked up, fire off this method */
            YesCalled();
        });

        /* Add Fire to the dictionary */
        keywords.Add("fire", () =>
        {
            /* When 'Fire' is picked up, fire off this method */
            FireCalled();
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

    /* Called when 'Fire' was picked up */
    void FireCalled()
    {
        /* If the player has a full shoot charge bar .. */
        if (ShootScript.currentCharge==100)
        {
            /* Create bullet if fire is said, rest is handled in bullet script */
            Instantiate(bulletPrefab, bullet.position, bullet.rotation);
            /* Reset the charge back to 0 */
            ShootScript.currentCharge = 0;
        }
    }

    /* Called when 'No' was picked up */
    void NoCalled()
    {   /* Load the menu scene */
        SceneManager.LoadScene("MenuScene");
    }

    /* Called when 'Yes' was picked up */
    void YesCalled()
    {
        if (SceneManager.GetActiveScene().name == "GameTwo")
        {
            /* Load the Bird Game scene again */
            SceneManager.LoadScene("GameTwo");
            Time.timeScale = 1f;
        } else {
            /* Load the Balloon Game scene again */
            SceneManager.LoadScene("MainScene");
            Time.timeScale = 1f;
        }
    }
}