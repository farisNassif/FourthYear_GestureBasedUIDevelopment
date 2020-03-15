using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This Script is for managing the main menu upon loading the application
// References : https://www.youtube.com/watch?v=zc8ac_qUXQY&t=583s (Brackeys tutorial on scene management)
// https://answers.unity.com/questions/1072521/set-gui-text-activeinactive-by-clicking-on-a-butto-1.html (Set GUI element to true or false with booleans)

public class MainMenuScript : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject mainMenu;

    void Start()
    {
        gameMenu.SetActive(false);
    }

    // Select Game function 
    public void SelectGame()
    {
        // Loads main scene (will be changed when updating menus)
        // SceneManager.LoadScene("MainScene");

        gameMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void IsSelected()
    {

    }
}
