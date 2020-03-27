using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Script for handling Game Over menu sequence */
public class GameOver : MonoBehaviour
{
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

    public void RestartGameOne()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }
}
