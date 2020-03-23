using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script for handling Game Over menu sequence
public class GameOver : MonoBehaviour
{

    BirdGameMusic music;

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
