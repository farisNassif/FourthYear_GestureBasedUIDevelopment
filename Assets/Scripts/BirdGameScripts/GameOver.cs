using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Script for handling Game Over menu sequence */
public class GameOver : MonoBehaviour
{
    /* Restart the game after finishing */
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
