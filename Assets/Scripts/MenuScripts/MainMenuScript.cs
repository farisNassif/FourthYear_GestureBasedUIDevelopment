using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This Script is for managing the main menu upon loading the application
// References : https://www.youtube.com/watch?v=zc8ac_qUXQY&t=583s (Brackeys tutorial on scene management)

public class MainMenuScript : MonoBehaviour
{
    // Select Game function 
    public void SelectGame()
    {
        // Loads main scene (will be changed when updating menus)
        SceneManager.LoadScene("MainScene");
    }
}
