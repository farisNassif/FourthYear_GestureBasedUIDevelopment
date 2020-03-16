using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform mHandMesh;

    private void Update()
    {
        mHandMesh.position = Vector3.Lerp(mHandMesh.position, transform.position, Time.deltaTime * 15.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (!collision.gameObject.CompareTag("Bubble"))
            return;

        ScoreManager.instance.AddScore(); // Deal with score
        SoundManagerScript.PopClip(); // Deal with pop sound

        Bubble bubble = collision.gameObject.GetComponent<Bubble>();
        StartCoroutine(bubble.Pop());
    }
}
