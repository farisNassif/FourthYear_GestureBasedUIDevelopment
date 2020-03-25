using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class to manage the behaviour and spawn of asteroids */
public class DeployAsteroid : MonoBehaviour
{
    public GameObject asteroidPrefab;
    /* How often they can change, alterable in inspector */
    public float respawnTime = 0.1f;
    private Vector2 screenBounds;

    /* Use this for initialization */
    void Start () {
        /* Get screen params and begin the asteroid wave */
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(asteroidWave());
    }

    /* Called be enumerator to create a barrage of enemy objects */
    private void spawnEnemy() {
        GameObject a = Instantiate(asteroidPrefab) as GameObject;
        a.transform.position = new Vector3(screenBounds.x * -7, Random.Range(-screenBounds.y, screenBounds.y));
    }
    
    /* Barrage of asteroids every (x) seconds */
    IEnumerator asteroidWave(){
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
