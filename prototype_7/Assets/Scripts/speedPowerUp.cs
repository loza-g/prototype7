using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedPowerUp : MonoBehaviour
{
    public GameObject bulletEater;
    public GameObject duckSpeed;
    public GameObject teleport;
    public GameObject swap;

    public float minSpawnTime_a = 1f;
    public float maxSpawnTime_a = 0.5f;
    public float minSpawnTime_b = 1f;
    public float maxSpawnTime_b = 3f;
    public float powerLifetime = 30f;
    public int maxSpawnCount_a = 3;
    public int maxSpawnCount_b = 3;
    public int maxSpawnCount_eater = 1;
    public int maxSpawnCount_teleport = 2;
    private int currentSpawnCount_eater;
    private int currentSpawnCount_speed;
    private int currentSpawnCount_teleport;
    private int currentSpawnCount_swap;
    // Start is called before the first frame update
    void Start()
    {
        currentSpawnCount_eater = 0;
        currentSpawnCount_speed = 0;
        currentSpawnCount_teleport = 0;
        currentSpawnCount_swap = 0;

        StartCoroutine(spawnEater());
        StartCoroutine(spawnDuckSpeed());
        StartCoroutine(spawnTeleport());
        StartCoroutine(spawnSwap());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator spawnEater()
    {
        //if max spawn count has not been reached and no other power up is spawned
        while (currentSpawnCount_eater < maxSpawnCount_eater)
        {
            yield return new WaitForSeconds(maxSpawnTime_a);
            InstantiatePowerUp(bulletEater);
            currentSpawnCount_eater++;
            Debug.Log("instantiated bullet eater power up. Current spawn count is: " + currentSpawnCount_eater);
          
        }
    }

    private IEnumerator spawnTeleport()
    {
        //if max spawn count has not been reached and no other power up is spawned
        while (currentSpawnCount_teleport < maxSpawnCount_teleport)
        {
            yield return new WaitForSeconds(maxSpawnTime_a);
            InstantiatePowerUp(teleport);
            currentSpawnCount_teleport++;
            Debug.Log("instantiated bullet eater power up. Current spawn count is: " + currentSpawnCount_teleport);

        }
    }

    private IEnumerator spawnSwap()
    {
        //if max spawn count has not been reached and no other power up is spawned
        while (currentSpawnCount_swap < maxSpawnCount_a)
        {
            yield return new WaitForSeconds(maxSpawnTime_a);
            InstantiatePowerUp(swap);
            currentSpawnCount_swap++;
            Debug.Log("instantiated bullet eater power up. Current spawn count is: " + currentSpawnCount_swap);

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
            Vector3 powerupPos = new Vector3(0, 0, 0);

            // GameObject[] ducks = GameObject.FindGameObjectsWithTag("Duck");
            
            // if (ducks.Length == 1)
            // {
            //     // spawn powerpoint within a 2 unit radius of the duck but not offscreen
            //     Vector3 duckPos = ducks[0].transform.position;

            //     powerupPos = new Vector3(Random.Range(duckPos.x - 2, duckPos.x + 2), Random.Range(duckPos.y - 2, duckPos.y + 2), 0);

            //     // while powerupPos is not onscreen repeat this process
            //     while (powerupPos.x < 0 || powerupPos.x > Screen.width || powerupPos.y < 0 || powerupPos.y > Screen.height)
            //     {
            //         powerupPos = new Vector3(Random.Range(duckPos.x - 2, duckPos.x + 2), Random.Range(duckPos.y - 2, duckPos.y + 2), 0);
            //     }
            // }
            // else
            // {
            //     // Generate random position within the screen
            //     Vector3 randomPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);

            //     // calculate moment of all duck positions
            //     powerupPos = new Vector3(0, 0, 0);
            //     foreach (GameObject duck in ducks)
            //     {
            //         powerupPos += duck.transform.position;
            //     }
            //     powerupPos /= ducks.Length;
            // }

        Vector3 randomPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(randomPosition);

        powerupPos = worldPosition;
        powerupPos.z = 0; // Make sure the object is at the same Z position as the camera

        // Instantiate  at the random position
        GameObject power = Instantiate(obj, powerupPos, Quaternion.identity);
        //Destroy(power, powerLifetime); // Destroy the object after _ seconds
        // if (power != null)
        // {
        //     StartCoroutine(DestroyAfterTime(power, powerLifetime));
        // }
       
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
