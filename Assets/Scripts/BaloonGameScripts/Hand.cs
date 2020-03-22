using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform mHandMesh;
    [HideInInspector]
    public static bool handsRecognized = false;

    private void Update()
    {
        mHandMesh.position = Vector3.Lerp(mHandMesh.position, transform.position, Time.deltaTime * 15.0f);
        handsRecognized = true;
    }

    /* When the hand collides with an object */
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        try 
        {
            /* Depending on the type of object hit, increment/decrement score. 
            *  Paramaters: 0 => Good Baloon | 1 => Bad Baloon | 2 => Rare Baloon */
            if (collision.gameObject.name == "PR_Bubble(Clone)")
            {
                ScoreManager.instance.UpdateScore(0);
            } else if (collision.gameObject.name == "PR_BadBaloon(Clone)")
            {
                ScoreManager.instance.UpdateScore(1);
            } 
                SoundManagerScript.PopClip(); // Play a random pop sound

            Bubble bubble = collision.gameObject.GetComponent<Bubble>();
            StartCoroutine(bubble.Pop());
        }
        catch(System.NullReferenceException exception) 
        {
            Debug.Log(exception);
        }
    }
}
