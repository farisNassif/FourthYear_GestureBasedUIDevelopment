using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Balloon class to define the spawning of balloons 
** Big help with the setting up => https://www.youtube.com/watch?v=6EkQA3GakFI&list=PLmc6GPFDyfw_Pouy2uRxVrEWFj4N1UOOp */
public class Bubble : MonoBehaviour
{
    /* Sprite initialization from the inspector */
    public Sprite mBubbleSprite;
    public Sprite mPopSprite;

    [HideInInspector]
    public BubbleManager mBubbleManager = null;

    /* Calculation required for movement of the balloons */
    private Vector3 mMovementDirection = Vector3.zero;

    /* Balloon renderer */
    private SpriteRenderer mSpriteRenderer = null;
    private Coroutine mCurrentChanger = null;

    private void Awake()
    {
        /* Initialize the balloon renderer */
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    /* On start .. */
    private void Start()
    {
        /* Direction the balloons flow */
        mCurrentChanger = StartCoroutine(DirectionChanger());
    }

    /* When they go out of bounds */
    private void OnBecameInvisible()
    {
        /* Change the position */
        transform.position = mBubbleManager.GetPlanePosition();
    }

    /* Called every frame */
    private void Update()
    {
        /* Movement of the balloon */
        transform.position += mMovementDirection * Time.deltaTime * 0.35f;

        /* Rotation of the balloon */
        transform.Rotate(Vector3.forward * Time.deltaTime * mMovementDirection.x * 20, Space.Self);
    }

    /* Enumerator that controls the popping of balloons */
    public IEnumerator Pop()
    {
        mSpriteRenderer.sprite = mPopSprite;

        StopCoroutine(mCurrentChanger);
        mMovementDirection = Vector3.zero;

        /* Every 0.5 seconds .. */
        yield return new WaitForSeconds(0.5f);

        /* Handle the direction and positions of the balloons */
        transform.position = mBubbleManager.GetPlanePosition();

        mSpriteRenderer.sprite = mBubbleSprite;
        mCurrentChanger = StartCoroutine(DirectionChanger());
    }

    /* Enumerator that handles directional changes */
    private IEnumerator DirectionChanger()
    {
        while (gameObject.activeSelf)
        {
            /* Calculate a new movement direction */
            mMovementDirection = new Vector2(Random.Range(-100, 100) * 0.01f, Random.Range(0, 100) * 0.01f);

            yield return new WaitForSeconds(5.0f);
        }
    }
}
