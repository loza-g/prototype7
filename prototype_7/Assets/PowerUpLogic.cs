using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{
    public GameObject circlePrefab;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;
    public float powerLifetime = 5f;
    public int maxSpawnCount = 3;
    private bool spawned;

    private int currentSpawnCount;

    // Start is called before the first frame update
    void Start()
    {
       
        spawned = false;
        currentSpawnCount = 0;

        StartCoroutine(spawnCircle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnCircle()
    {
        //if max spawn count has not been reached and no other power up is spawned
        if(currentSpawnCount < maxSpawnCount && !spawned)
        {
            yield return new WaitForSeconds(maxSpawnTime);
            InstantiatePowerUp();
            currentSpawnCount++;
            Debug.Log("instantiated power up. Current spawn count is: " + currentSpawnCount);
            spawned = true;
        }
    }

    //private IEnumerator SpawnCircle()
    //{
    //    while (currentSpawnCount < maxSpawnCount)
    //    {
    //        if (!spawned)
    //        {
    //            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
    //            InstantiatePowerUp();
    //            currentSpawnCount++;
    //            Debug.Log("instantiated power up. Current spawn count is: " + currentSpawnCount);
    //            spawned = true;
    //        }
           
    //    }
    //}

    private void InstantiatePowerUp()
    {
        // Generate random position within the screen
        Vector3 randomPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(randomPosition);
        worldPosition.z = 0; // Make sure the object is at the same Z position as the camera

        // Instantiate circlePrefab at the random position
        GameObject power = Instantiate(circlePrefab, worldPosition, Quaternion.identity);
        Destroy(power, powerLifetime); // Destroy the object after circleLifetime seconds
    }
}
