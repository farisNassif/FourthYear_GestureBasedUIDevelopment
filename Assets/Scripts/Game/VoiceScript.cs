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
        Debug.Log("Fire called");

        // need to Instantiate or enable an existing object here 
        // gameObject.SetActive(true);

        // set velocity of object here or maybe in another script
        // vel = 2.5f;

        // collisions of object to be set here as well
        /* if(gameObject.collision)
        {
            Destroy(gameObject);
        }
        */
    }

}