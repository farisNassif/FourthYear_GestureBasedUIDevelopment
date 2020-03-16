using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Constructor class script for generating a random number */
public class RandomGenerator : MonoBehaviour
{
    private static System.Random random = new System.Random();
    public int randomNumber;
    private int minNum;
    private int maxNum;

    /* Constructor that sets the randomNumber int to a random num between x and y */
    public RandomGenerator(int min, int max) {
        minNum = min;
        maxNum = max;

        randomNumber = random.Next(minNum, maxNum);
    }
}
