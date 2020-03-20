using System.Collections;
using UnityEngine;
using Microsoft.Kinect.VisualGestureBuilder;
using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

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
    /* Gesture object which will store our flap gesture after it's loaded from the Database */
    Gesture Flap, Swipe, TurnLeft, TurnRight, Hover; 
    PlayerMovement PM = new PlayerMovement();
    /* Build the path to the flap gesture database */
    string databasePathFlap = System.IO.Path.Combine(Application.streamingAssetsPath, "Flap.gbd"); // No longer using
    string databasePathSwipe = System.IO.Path.Combine(Application.streamingAssetsPath, "Swipe.gbd");
    string databasePathTurnLeft= System.IO.Path.Combine(Application.streamingAssetsPath, "TurnLeft.gbd");
    string databasePathTurnRight = System.IO.Path.Combine(Application.streamingAssetsPath, "TurnRight.gbd");
    string databasePathHover = System.IO.Path.Combine(Application.streamingAssetsPath, "Hover.gbd");

    /* Start is called before the first frame update */
    void Start()
    {
        
        /* Get the sensor and open it */
        kinect = KinectSensor.GetDefault();
        kinect.Open();

        /* Frame reader and frame source assignments required for picking up gesture confidence */
        vgbFrameSource = VisualGestureBuilderFrameSource.Create(kinect, 0);
        vgbFrameReader = vgbFrameSource.OpenReader();

        /* Database path assignment, for each gesture, add the gesture (Load in Swipe) */
        using (VisualGestureBuilderDatabase vgbDb = VisualGestureBuilderDatabase.Create(databasePathSwipe))
        {
            foreach (var gesture in vgbDb.AvailableGestures)
            {
                Debug.Log(vgbFrameSource.Gestures.Count); // Should be 0
                vgbFrameSource.AddGesture(gesture);
                Debug.Log(vgbFrameSource.Gestures.Count); // Should now be 1??

                /* If 'Swipe.gdb' is in the Assets/StreamingAssets folder .. */
                if(gesture.Name.Equals("Swipe"))
                {                           
                    Swipe = gesture;
                    Debug.Log(Swipe.Name + " gesture loaded from gesture database");
                } 
    
            }    
        }

        /* Database path assignment, for each gesture, add the gesture (Load in Turn Left) */
        using (VisualGestureBuilderDatabase vgbDb = VisualGestureBuilderDatabase.Create(databasePathTurnLeft))
        {
            foreach (var gesture in vgbDb.AvailableGestures)
            {
                Debug.Log(vgbFrameSource.Gestures.Count); // Should be 0
                vgbFrameSource.AddGesture(gesture);
                Debug.Log(vgbFrameSource.Gestures.Count); // Should now be 1??

                /* If 'TurnLeft.gdb' is in the Assets/StreamingAssets folder .. */
                if(gesture.Name.Equals("TurnLeft"))
                {                           
                    TurnLeft = gesture;
                    Debug.Log(TurnLeft.Name + " gesture loaded from gesture database");
                } 
    
            }    
        }

        /* Database path assignment, for each gesture, add the gesture (Load in Turn Right) */
        using (VisualGestureBuilderDatabase vgbDb = VisualGestureBuilderDatabase.Create(databasePathTurnRight))
        {
            foreach (var gesture in vgbDb.AvailableGestures)
            {
                Debug.Log(vgbFrameSource.Gestures.Count); // Should be 0
                vgbFrameSource.AddGesture(gesture);
                Debug.Log(vgbFrameSource.Gestures.Count); // Should now be 1??

                /* If 'TurnRight.gdb' is in the Assets/StreamingAssets folder .. */
                if(gesture.Name.Equals("TurnRight"))
                {                           
                    TurnRight = gesture;
                    Debug.Log(TurnRight.Name + " gesture loaded from gesture database");
                } 
    
            }    
        }

        /* Database path assignment, for each gesture, add the gesture (Load in Hover) */
        using (VisualGestureBuilderDatabase vgbDb = VisualGestureBuilderDatabase.Create(databasePathHover))
        {
            foreach (var gesture in vgbDb.AvailableGestures)
            {
                Debug.Log(vgbFrameSource.Gestures.Count); // Should be 0
                vgbFrameSource.AddGesture(gesture);
                Debug.Log(vgbFrameSource.Gestures.Count); // Should now be 1??

                /* If 'TurnRight.gdb' is in the Assets/StreamingAssets folder .. */
                if(gesture.Name.Equals("Hover"))
                {                           
                    Hover = gesture;
                    Debug.Log(Hover.Name + " gesture loaded from gesture database");
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
                    DiscreteGestureResult TurnLeftResult;
                    DiscreteGestureResult TurnRightResult;
                    DiscreteGestureResult HoverResult;
                    
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
                        Debug.Log("Swiped!");
                        /* Do menu stuff here .. */

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
                    }
                    else if (TurnRightResult.Confidence > 0.2)
                    {
                        PlayerMovement.flyingDown = true;
                        PlayerMovement.flyingUp = false;
                        PlayerMovement.hover = false;
                    }
                    else if(HoverResult.Confidence > 0.1)
                    {
                        PlayerMovement.hover = true;
                        PlayerMovement.flyingDown = false;
                        PlayerMovement.flyingUp = false;                       
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

    /* Update isn't really needed in this class, leaving just incase unity goes mad */
    void Update()
    {
        
    }
}
