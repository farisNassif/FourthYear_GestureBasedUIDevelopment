using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script for handling Game Over menu sequence
public class GameOver : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameTwo");
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
