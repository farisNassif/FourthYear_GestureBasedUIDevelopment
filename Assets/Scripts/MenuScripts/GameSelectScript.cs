﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Windows.Kinect;

// Script for loading into game one

public class GameSelectScript : MonoBehaviour
{
    
    public void SelectGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
