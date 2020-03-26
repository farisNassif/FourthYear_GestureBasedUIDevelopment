using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* At the start we had bubbles instead of balloons, hence the Bubble/BubbleManager classes
** Manager for the balloons & spawning of balloons + initialization */
public class BubbleManager : MonoBehaviour
{
    /* Objects required for initialization */
    public GameObject mBubblePrefab;
    public GameObject mBadBubblePrefab;
    /* Text alerting user if the hands were recognized yet or not */
    public Text handRecog;

    [HideInInspector]

    /* List for good and bad balloons */
    private List<Bubble> GoodBubbles = new List<Bubble>();
    private List<Bubble> BadBubbles = new List<Bubble>();

    /* Used to help get the params of the screen for spawning */
    private Vector2 mBottomLeft = Vector2.zero;
    private Vector2 mTopRight = Vector2.zero;

    /* So it only executes once */
    private bool executedOnce = false;

    /* Called once at the start */
    private void Start()
    {
        /* Until hands are detected display the hands information message */
        handRecog.gameObject.SetActive(true);
    }
    
    private void Awake()
    {
        /* Bounding values */
        mBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane));
        mTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight / 2, Camera.main.farClipPlane));
    }

    /* Called each frame */
    private void Update() {
        /* If hands were recognized, GameManager is ready to start and if this wasn't already ran, run it */
        if (executedOnce == false && Hand.handsRecognized == true) {
            /* Set the hand text to false, since hands are now picked up */
            handRecog.gameObject.SetActive(false);
            /* Start spawning objects */
            StartCoroutine(CreateBubbles());
            /* To ensure multiple coroutines don't execute */
            executedOnce = true; 
        }
    }

    /* Used to control spawn area of baloons */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane)), 0.5f);
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.farClipPlane)), 0.5f);
    }

    /* Get the plane position for spawning */
    public Vector3 GetPlanePosition()
    {
        /* Random Position */
        float targetX = Random.Range(mBottomLeft.x, mTopRight.x);
        float targetY = Random.Range(mBottomLeft.y, mTopRight.y);

        return new Vector3(targetX, targetY, 0);
    }

    /* Enumerator for spawning balloons */
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
