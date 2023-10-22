using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehavior : MonoBehaviour
{

    // screen edges in world space
    float s;
    float t;
    float u;
    float v;

    public int speed = 3;


    void Awake()
    {
        s = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        t = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        u = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        v = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // apply light force to the rigidbody in random directions

        int[] dirs = {-1,1};
        var xDir = dirs[Random.Range(0,2)];
        var yDir = dirs[Random.Range(0,2)];

        GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * 5 * xDir, speed * 5 * yDir));

        // dont let the duck go past the screen
        // get the position of the duck
        Vector3 duckPos = transform.position;

        // if the duck is past the left edge of the screen
        if (duckPos.x < s)
        {
            // set the duck's x position to s
            duckPos.x = s + 0.5f;
            // set the duck's position to the new position
            transform.position = duckPos;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
        // if the duck is past the right edge of the screen
        if (duckPos.x > t)
        {
            // set the duck's x position to t
            duckPos.x = t -0.5f;
            // set the duck's position to the new position
            transform.position = duckPos;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
        // if the duck is past the top edge of the screen
        if (duckPos.y > v)
        {
            // set the duck's y position to v
            duckPos.y = v - 0.5f;
            // set the duck's position to the new position
            transform.position = duckPos;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
        // if the duck is past the bottom edge of the screen
        if (duckPos.y < u)
        {
            // set the duck's y position to u
            duckPos.y = u + 0.5f;
            // set the duck's position to the new position
            transform.position = duckPos;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }

        // remove all force from the duck
    }
}
