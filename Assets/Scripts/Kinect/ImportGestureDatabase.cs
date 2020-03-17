using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Kinect.VisualGestureBuilder;
using Windows.Kinect;


public class ImportGestureDatabase : MonoBehaviour
{
    VisualGestureBuilderFrameSource vgbFrameSource;
    VisualGestureBuilderFrameReader vgbFrameReader;
    KinectSensor kinect = null;
    private Gesture gesture;
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
                if(g.Name.Equals("Flap"))
                {
                    gesture = g;
                    Debug.Log(vgbFrameSource.Gestures.Count);
                    vgbFrameSource.AddGesture(gesture);
                    Debug.Log(vgbFrameSource.Gestures.Count);
                    Debug.Log(gesture.Name + " gesture loaded from gesture database");
                } else {
                    Debug.Log("shit didnt work D:");
                }
            }    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
