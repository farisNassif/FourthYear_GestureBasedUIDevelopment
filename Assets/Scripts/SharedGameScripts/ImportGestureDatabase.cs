using System.Collections;
using UnityEngine;
using Microsoft.Kinect.VisualGestureBuilder;
using Windows.Kinect;
using Joint = Windows.Kinect.Joint;
using UnityEngine.SceneManagement;

/* Class that builds the path to the gesture Database, loads in and saves those pre recorded gestures then listens to see if one was picked up.
** The code in this class was adapted with the help from the book 'Beginning Microsoft Kinect for Windows SDK 2.0' (Page 240-245)
*/
public class ImportGestureDatabase : MonoBehaviour
{
    /* Gesture var initializers */
    VisualGestureBuilderFrameSource vgbFrameSource;
    Windows.Kinect.BodyFrameSource bodyFrameSource;
    VisualGestureBuilderFrameReader vgbFrameReader;
    Windows.Kinect.BodyFrameReader bodyFrameReader;
    /* Kinect Ref */
    Windows.Kinect.KinectSensor kinect;
    
    /* Gesture object which will store the gestures after they're loaded from the Database */
    Gesture Flap, Swipe, TurnLeft, TurnRight, Hover; 
    PlayerMovement PM = new PlayerMovement();
    /* Array to hold gesture database file names for more efficient access */
    string[] gesturePaths = new string[4] {
        "Swipe.gbd",
        "TurnLeft.gbd",
        "TurnRight.gbd",
        "Hover.gbd",
    };
    /* So swipe isn't spammed */
    public bool recentlySwiped = false;

    /* On start/play, make sure the kinect is open and read in all saved gestures from the .gbd files */
    void Start()
    {
        /* Get the sensor and open it */
        kinect = KinectSensor.GetDefault();
        kinect.Open();

        /* Frame reader and frame source assignments required for picking up gesture confidence */
        vgbFrameSource = VisualGestureBuilderFrameSource.Create(kinect, 0);
        vgbFrameReader = vgbFrameSource.OpenReader();

        /* Loop four times, once for each gesture to be loaded in */
        for (int i = 0 ; i < 4 ; i++) {
            /* Depending on loop will change gesturePaths to Swipe/TurnLeft/TurnRight/Hover to load in individually */
            string databasePath = System.IO.Path.Combine(Application.streamingAssetsPath, gesturePaths[i]);

            /* Database path assignment, for each gesture, add the gesture (Load in Swipe) */
            using (VisualGestureBuilderDatabase vgbDb = VisualGestureBuilderDatabase.Create(databasePath))
            {
                foreach (var gesture in vgbDb.AvailableGestures)
                {
                    Debug.Log(vgbFrameSource.Gestures.Count); // Should be 0
                    vgbFrameSource.AddGesture(gesture);
                    Debug.Log(vgbFrameSource.Gestures.Count); // Should now be 1??

                    /* Get all the gesture names and assign them to local gesture variables */
                    if(gesture.Name.Equals("Swipe"))
                    {                           
                        Swipe = gesture;
                        Debug.Log(Swipe.Name + " gesture loaded from gesture database");
                    } 
                    else if (gesture.Name.Equals("TurnLeft"))
                    {
                        TurnLeft = gesture;
                        Debug.Log(TurnLeft.Name + " gesture loaded from gesture database");
                    } 
                    else if (gesture.Name.Equals("TurnRight"))
                    {
                        TurnRight = gesture;
                        Debug.Log(TurnRight.Name + " gesture loaded from gesture database");
                    } 
                    else if (gesture.Name.Equals("Hover"))
                    {
                        Hover = gesture;
                        Debug.Log(Hover.Name + " gesture loaded from gesture database");
                    }
                }    
            }
        }
        
        /* Begin reading and looking for a body */
        bodyFrameSource = kinect.BodyFrameSource;
        bodyFrameReader = bodyFrameSource.OpenReader();
        bodyFrameReader.FrameArrived += bodyFrameArrived;

        /* Once a body was picked up, look at the specific frames and see if they match something (like one of my gestures) */
        vgbFrameReader = vgbFrameSource.OpenReader();
        vgbFrameReader.IsPaused = false;
        vgbFrameReader.FrameArrived += vgbFrameArrived;
    }

    /* Method that handles body tracking 
    ** Code in this method was constructed with the help of Beginning Microsoft Kinect for Windows SDK 2.0 (Page 23x)
    */
    private void bodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
    {
        if (!vgbFrameSource.IsTrackingIdValid)
        {
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {                  
                    Body[] bodies = new Body[6];
                    bodyFrame.GetAndRefreshBodyData(bodies);
                    Body closestBody = null;

                    foreach (Body b in bodies)
                    {
                        if (b.IsTracked)
                        {
                            if (closestBody == null)
                            {   
                                closestBody = b;
                            }
                            else
                            {
                                Joint newHeadJoint = b.Joints[JointType.Head];
                                Joint oldHeadJoint = closestBody.Joints[JointType.Head];
                                if (newHeadJoint.TrackingState == TrackingState.Tracked && newHeadJoint.Position.Z < oldHeadJoint.Position.Z)
                                {
                                    closestBody = b;
                                }
                            }
                        }
                    }
                    if (closestBody != null)
                    {
                        vgbFrameSource.TrackingId = closestBody.TrackingId;
                    }
                }
            }
        }
    }

    /* Analyse specific frames and compare against pre saved gestures and analyse confidence */
    void vgbFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
    {
        /* Acquire and analyse the frame .. */
        using (var vgbFrame = e.FrameReference.AcquireFrame())
        {
            /* As long as it's not null .. */
            if (vgbFrame != null)
            {
                var results = vgbFrame.DiscreteGestureResults;

                /* If there are actually results in the frame */
                if (results != null && results.Count > 0)
                {
                    /* Gesture result declaration */
                    DiscreteGestureResult SwipeResult;
                    /* The following three are continious but VS gives out when I declare them as so.
                    ** It doesn't make a difference though it still works as intended.
                    */
                    DiscreteGestureResult TurnLeftResult, TurnRightResult, HoverResult;
                    
                    /* ----- Compare the frame against the gestures -----*/

                    /* Discrete gesture data results */
                    results.TryGetValue(Swipe, out SwipeResult);
                    /* Continious gesture data results */
                    results.TryGetValue(TurnLeft, out TurnLeftResult);
                    results.TryGetValue(TurnRight, out TurnRightResult);
                    results.TryGetValue(Hover, out HoverResult);

                    /* ----- Listening, or detecting to see if the user made a gesture like a loaded one from the Database .. ----- */

                    /* If it's 98% sure a swipe was just made */
                    if (SwipeResult.Confidence >= 0.98)
                    {
                        /* If Player is in the menu scene and hadn't recently swiped .. */
                        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName ("MenuScene") && GameSelectScript.recentlySwiped == false)
                        {
                            /* Make swipe True and chill for 3 seconds 
                            ** Reference GameSelectScript object = true, will now go back in the menu */
                            StartCoroutine(WaitForThreeSeconds());
                           
                        }
                    }

                    /* The following 3 if statements manages the flying status of the bird in Playermovement.
                    ** Only one condition can be true at one given time, allowing for smooth navigation of the bird
                    ** as long as the user is performing the real time continious gesture.
                    ** The flying gestures were trained on both ideal and worst case scenario data/videos to ensure maximum accuracy.
                    */
                    if (TurnLeftResult.Confidence > 0.2)
                    {
                        PlayerMovement.flyingUp = true;
                        PlayerMovement.flyingDown = false;
                        PlayerMovement.hover = false;  
                        Debug.Log("Turning Left(Up)!");
                    }
                    else if (TurnRightResult.Confidence > 0.2)
                    {
                        PlayerMovement.flyingDown = true;
                        PlayerMovement.flyingUp = false;
                        PlayerMovement.hover = false;
                        Debug.Log("Turning Right(Down)!");
                    }
                    else if(HoverResult.Confidence > 0.1)
                    {
                        PlayerMovement.hover = true;
                        PlayerMovement.flyingDown = false;
                        PlayerMovement.flyingUp = false;      
                        Debug.Log("Hovering!");                 
                    }
                }
            }
        }
    }

    /* IEnumerator that delays for 1 second.
    ** This can be handy when we only really need 1 gesture to execute in a given second.
    */
    private IEnumerator WaitForSeconds()
    {
        /* Pause the frame reader, wait 1 second and unpause it */
        vgbFrameReader.IsPaused = true;
        yield return new WaitForSeconds(1);
        vgbFrameReader.IsPaused = false;
    }

    /* IEnumerator that delays for 3 second.
    ** This can be handy when we only really need 1 gesture to execute in a given second.
    */
    private IEnumerator WaitForThreeSeconds()
    {
        /* Reference GameSelectScript object = true, will now go back in the menu */
        GameSelectScript.recentlySwiped = true;
        /* Pause the frame reader, wait 3 seconds and unpause it */
        vgbFrameReader.IsPaused = true;
        yield return new WaitForSeconds(3);
        vgbFrameReader.IsPaused = false;
        GameSelectScript.recentlySwiped = false;
    }
}
