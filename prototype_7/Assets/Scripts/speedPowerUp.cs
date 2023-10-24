using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedPowerUp : MonoBehaviour
{
    public GameObject bulletEater;
    public GameObject duckSpeed;

    public float minSpawnTime_a = 1f;
    public float maxSpawnTime_a = 3f;
    public float minSpawnTime_b = 1f;
    public float maxSpawnTime_b = 3f;
    public float powerLifetime = 30f;
    public int maxSpawnCount_a = 3;
    public int maxSpawnCount_b = 3;

    private int currentSpawnCount_eater;
    private int currentSpawnCount_speed;
    // Start is called before the first frame update
    void Start()
    {
        currentSpawnCount_eater = 0;
        currentSpawnCount_speed = 0;


        StartCoroutine(spawnEater());
        StartCoroutine(spawnDuckSpeed());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator spawnEater()
    {
        //if max spawn count has not been reached and no other power up is spawned
        while (currentSpawnCount_eater < maxSpawnCount_a)
        {
            yield return new WaitForSeconds(maxSpawnTime_a);
            InstantiatePowerUp(bulletEater);
            currentSpawnCount_eater++;
            Debug.Log("instantiated bullet eater power up. Current spawn count is: " + currentSpawnCount_eater);
          
        }
    }

    private IEnumerator spawnDuckSpeed()
    {
        //if max spawn count has not been reached and no other power up is spawned
        while (currentSpawnCount_speed < maxSpawnCount_b)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime_b, maxSpawnTime_b));
            InstantiatePowerUp(duckSpeed);
            currentSpawnCount_speed++;
            Debug.Log("instantiated speed multiplier power up. Current spawn count is: " + currentSpawnCount_speed);

        }
    }

    private void InstantiatePowerUp(GameObject obj)
    {
        // Generate random position within the screen
        Vector3 randomPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(randomPosition);
        worldPosition.z = 0; // Make sure the object is at the same Z position as the camera

        // Instantiate  at the random position
        GameObject power = Instantiate(obj, worldPosition, Quaternion.identity);
        //Destroy(power, powerLifetime); // Destroy the object after _ seconds
        StartCoroutine(DestroyAfterTime(power, powerLifetime));
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
