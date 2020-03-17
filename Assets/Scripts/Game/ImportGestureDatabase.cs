using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Kinect.VisualGestureBuilder;
using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class ImportGestureDatabase : MonoBehaviour
{
    VisualGestureBuilderFrameSource vgbFrameSource;
    Windows.Kinect.BodyFrameSource bodyFrameSource;
    VisualGestureBuilderFrameReader vgbFrameReader;
    Windows.Kinect.BodyFrameReader bodyFrameReader;
    Windows.Kinect.KinectSensor kinect;
    private Gesture gesture;
    Gesture Flap; 
    /* Build the path to the flap gesture database */
    string databasePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Flap.gbd");
    // Start is called before the first frame update
    void Start()
    {
        kinect = KinectSensor.GetDefault();
        kinect.Open();
        vgbFrameSource = VisualGestureBuilderFrameSource.Create(kinect, 0);
        vgbFrameReader = vgbFrameSource.OpenReader();

        using (VisualGestureBuilderDatabase vgbDb = VisualGestureBuilderDatabase.Create(databasePath))
        {
            foreach (var g in vgbDb.AvailableGestures)
            {
                Debug.Log(vgbFrameSource.Gestures.Count); // Should be 0
                vgbFrameSource.AddGesture(g);
                Debug.Log(vgbFrameSource.Gestures.Count); // Should now be 1??

                if(g.Name.Equals("Flap"))
                {                           
                    Flap = g;
                    Debug.Log(Flap.Name + " gesture loaded from gesture database");
                } else {
                    Debug.Log("shit didnt work D:");
                }
            }    
        }

        bodyFrameSource = kinect.BodyFrameSource;
        bodyFrameReader = bodyFrameSource.OpenReader();
        bodyFrameReader.FrameArrived += bodyFrameArrived;

        vgbFrameReader = vgbFrameSource.OpenReader();
        vgbFrameReader.FrameArrived += vgbFrameArrived;
    }

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

    void vgbFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
    {
        using (var vgbFrame = e.FrameReference.AcquireFrame())
        {
            if (vgbFrame != null)
            {
                var results = vgbFrame.DiscreteGestureResults;
                if (results != null && results.Count > 0)
                {
                        DiscreteGestureResult FlapResult;
                        results.TryGetValue(Flap, out FlapResult);
                       // Debug.Log("Result not null, conf = " + FlapResult.Confidence);

                    if (FlapResult.Confidence > 0.7)
                    {
                        Debug.Log(FlapResult.Confidence);
                        Debug.Log("I'M FLAAAAAAPPPPING");
                        //float progressResult = vgbFrame.ContinuousGestureResults[gestureProgress];
                        //Do something with progressResult
                    }
                    else
                    {
                       // Debug.Log("AAAAAAAAAA BAAAAAAAADD");
                        //set progress in front end to 0
                    }
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
