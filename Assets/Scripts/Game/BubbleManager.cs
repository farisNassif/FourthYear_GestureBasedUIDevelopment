using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject mBubblePrefab;

    public GameObject mBadBubblePrefab;

    [HideInInspector]
    private List<Bubble> GoodBubbles = new List<Bubble>();
    private List<Bubble> BadBubbles = new List<Bubble>();

    private Vector2 mBottomLeft = Vector2.zero;
    private Vector2 mTopRight = Vector2.zero;

    private bool executedOnce = false;

    private void Awake()
    {
        // Bounding values
        mBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane));
        mTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, Camera.main.farClipPlane));
    }

    private void Update() {
        /* If hands were recognized, GameManager is ready to start and if this wasn't already ran, run it */
        if (executedOnce == false) {
            StartCoroutine(CreateBubbles());
            executedOnce = true; // To ensure multiple coroutines don't execute
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane)), 0.5f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.farClipPlane)), 0.5f);
    }

    public Vector3 GetPlanePosition()
    {
        /* Random Position */
        float targetX = Random.Range(mBottomLeft.x, mTopRight.x);
        float targetY = Random.Range(mBottomLeft.y, mTopRight.y);

        return new Vector3(targetX, targetY, 0);
    }

    public IEnumerator CreateBubbles()
    {
        while (GoodBubbles.Count < 100 && BadBubbles.Count < 100)
        {
            /* Generates any number from 0 - 9 */
            RandomGenerator random = new RandomGenerator(0,10); 

            /* If the number is 2, 3, 4, 5, 6, 7, 8, 9 generate a normal object (70% chance) */
            if (random.randomNumber > 1) {
                /* Create and add positively scoring object */
                GameObject newBubbleObject = Instantiate(mBubblePrefab, GetPlanePosition(), Quaternion.identity, transform);
                Bubble newBubble = newBubbleObject.GetComponent<Bubble>();

                /* Setup positive object */
                newBubble.mBubbleManager = this;
                GoodBubbles.Add(newBubble);               
            } 

            /* If the number is 0, or 1 generate a negative object (30% chance) */
            else 
            {
                /* Create and add negatively scoring object */
                GameObject newBadBubbleObject = Instantiate(mBadBubblePrefab, GetPlanePosition(), Quaternion.identity, transform);
                Bubble badBubble = newBadBubbleObject.GetComponent<Bubble>();

                /* Setup negative object */
                badBubble.mBubbleManager = this;
                BadBubbles.Add(badBubble); 
            }
                /* Rate of spawn */
                yield return new WaitForSeconds(0.3f);
        }
    }
}
