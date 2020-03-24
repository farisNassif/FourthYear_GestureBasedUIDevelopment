using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployAsteroid : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float respawnTime = 0.1f;
    private Vector2 screenBounds;

    // Use this for initialization
    void Start () {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(asteroidWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawnEnemy() {
        GameObject a = Instantiate(asteroidPrefab) as GameObject;
        a.transform.position = new Vector3(screenBounds.x * -7, Random.Range(-screenBounds.y, screenBounds.y));
    }
    
    IEnumerator asteroidWave(){
        while(true) {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
