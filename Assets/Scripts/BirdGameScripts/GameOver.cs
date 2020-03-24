using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Script for handling Game Over menu sequence */
public class GameOver : MonoBehaviour
{
<<<<<<< HEAD
    /* Restart the game after finishing */
=======

    void Start()
    {
        
    }

    void Update()
    {
        
    }

>>>>>>> 437ada52eefa90397391b2c895df18a31499d47d
    public void Restart()
    {
        SceneManager.LoadScene("GameTwo");
        Time.timeScale = 1f;
    }

    /* Ragequit to the main menu */
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
