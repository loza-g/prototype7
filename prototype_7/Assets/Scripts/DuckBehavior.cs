using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DuckBehavior : MonoBehaviour
{

    // teleport shadow
    public GameObject tS;
    public GameManagerBehavior gameMgr;
    // screen edges in world space
    float s;
    float t;
    float u;
    float v;
    public float teleportDelay = 1f;

    public int speed = 6;

    enum direction { up, down, left, right, diagonalUpLeft, diagonalUpRight, diagonalDownLeft, diagonalDownRight };
    direction currentDirection;

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
        gameMgr = GameObject.Find("Game Manager").GetComponent<GameManagerBehavior>();
        direction[] startingDirections = {direction.up, direction.diagonalUpLeft, direction.diagonalUpRight};
        currentDirection = startingDirections[Random.Range(0, 3)];
        StartCoroutine(directionManager());
        
    }

    // Update is called once per frame
    void Update()
    {
        // apply light force to the rigidbody in random directions

        // int[] dirs = {-1,1};
        // var xDir = dirs[Random.Range(0,2)];
        // var yDir = dirs[Random.Range(0,2)];

        // GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * 5 * xDir, speed * 5 * yDir));

        // dont let the duck go past the screen
        // get the position of the duck
        Vector3 duckPos = transform.position;


        switch (currentDirection)
        {
            // apply directions to duck using transform position. Not using rigidbody
            case direction.up:
                duckPos.y += speed * Time.deltaTime;
                transform.position = duckPos;
                break;
            case direction.down:
                duckPos.y -= speed * Time.deltaTime;
                transform.position = duckPos;
                break;
            case direction.left:
                duckPos.x -= speed * Time.deltaTime;
                transform.position = duckPos;
                break;
            case direction.right:
                duckPos.x += speed * Time.deltaTime;
                transform.position = duckPos;
                break;
            case direction.diagonalUpLeft:
                duckPos.x -= speed * Time.deltaTime;
                duckPos.y += speed * Time.deltaTime;
                transform.position = duckPos;
                break;
            case direction.diagonalUpRight:
                duckPos.x += speed * Time.deltaTime;
                duckPos.y += speed * Time.deltaTime;
                transform.position = duckPos;
                break;
            case direction.diagonalDownLeft:
                duckPos.x -= speed * Time.deltaTime;
                duckPos.y -= speed * Time.deltaTime;
                transform.position = duckPos;
                break;
            case direction.diagonalDownRight:
                duckPos.x += speed * Time.deltaTime;
                duckPos.y -= speed * Time.deltaTime;
                transform.position = duckPos;
                break;

        }

        // if the duck is past the left edge of the screen
        if (duckPos.x - 0.5 < s)
        {
            // set the duck's x position to s
            duckPos.x = s + 0.5f;
            // set the duck's position to the new position
            transform.position = duckPos;
            int dirInt = Random.Range(0, 8);
            currentDirection = (direction)dirInt;

        }
        // if the duck is past the right edge of the screen
        if (duckPos.x + 0.5 > t)
        {
            // set the duck's x position to t
            duckPos.x = t -0.5f;
            // set the duck's position to the new position
            transform.position = duckPos;
            int dirInt = Random.Range(0, 8);
            currentDirection = (direction)dirInt;

        }
        // if the duck is past the top edge of the screen
        if (duckPos.y + 0.25 > v)
        {
            // set the duck's y position to v
            duckPos.y = v - 0.25f;
            // set the duck's position to the new position
            transform.position = duckPos;
            int dirInt = Random.Range(0, 8);
            currentDirection = (direction)dirInt;

        }
        // if the duck is past the bottom edge of the screen
        if (duckPos.y - 0.25 < u)
        {
            // set the duck's y position to u
            duckPos.y = u + 0.25f;
            // set the duck's position to the new position
            transform.position = duckPos;
            int dirInt = Random.Range(0, 8);
            currentDirection = (direction)dirInt;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bulletEater"))
        {
            if(gameMgr.GetBulletCount() > 0)
            {
                gameMgr.UpdateBulletCount(2);
                Debug.Log("Duck ate 2 bullets. current bullet count: " + gameMgr.GetBulletCount());
                if(collision.gameObject != null) {
                    Destroy(collision.gameObject);
                }
            }
            
        }

        if (collision.gameObject.CompareTag("duckSpeed"))
        {
            speed += 2;
            if (collision.gameObject != null)
            {
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("duckTeleport"))
        {
            if (collision.gameObject != null)
            {
                Destroy(collision.gameObject);
            }
            Vector2 newPos = chooseLocation();
            //maybe add random yield time here 
            StartCoroutine(teleportLoop());

        }

        if (collision.gameObject.CompareTag("reticleSwap"))
        {
            //swap mouse and duck positions
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 duckPos = transform.position;
            duckPos.z = 0;
            transform.position = mousePos;

            Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(duckPos));
            if (collision.gameObject != null)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    IEnumerator teleportLoop() 
    {
        while (true)
        {
            Vector3 newPos = chooseLocation();
            Instantiate(tS, newPos, Quaternion.identity);
            yield return new WaitForSeconds(teleportDelay);
            transform.position = newPos;
           
        }
    }   

    IEnumerator directionManager() 
    {

        while (true)
        {
            int time = Random.Range(1, 4);
            yield return new WaitForSeconds(time);
            int dirInt = Random.Range(0, 8);
            currentDirection = (direction) dirInt;
        }
    }

    Vector3 chooseLocation()
    {
        // choose a random location within the screen to spawn the duck so that the whole sprite will be on the screen
        // get the screen width and height
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // get the width and height of the duck sprite
        float duckWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        float duckHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        // get the x and y coordinates of the bottom left corner of the duck sprite
        float duckX = transform.position.x - duckWidth / 2;
        float duckY = transform.position.y - duckHeight / 2;

        // get the x and y coordinates of the top right corner of the duck sprite
        float duckX2 = transform.position.x + duckWidth / 2;
        float duckY2 = transform.position.y + duckHeight / 2;

        // get the x and y coordinates of the bottom left corner of the screen
        float screenX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        // get the x and y coordinates of the top right corner of the screen
        float screenX2 = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, 0, 0)).x;
        float screenY2 = Camera.main.ScreenToWorldPoint(new Vector3(0, screenHeight, 0)).y;

        // get the width and height of the screen
        float screenWidth2 = screenX2 - screenX;
        float screenHeight2 = screenY2 - screenY;

        // get the width and height of the duck sprite
        float duckWidth2 = duckX2 - duckX;
        float duckHeight2 = duckY2 - duckY;

        // get the width and height of the screen minus the width and height of the duck sprite
        float screenWidth3 = screenWidth2 - duckWidth2;
        float screenHeight3 = screenHeight2 - duckHeight2;

        // get the x and y coordinates of the bottom left corner of the screen plus half the width and height of the duck sprite
        float screenX3 = screenX + duckWidth2 / 2;
        float screenY3 = screenY + duckHeight2 / 2;

        // get the x and y coordinates of the top right corner of the screen minus half the width and height of the duck sprite
        float screenX4 = screenX2 - duckWidth2 / 2;
        float screenY4 = screenY2 - duckHeight2 / 2;

        // get the x and y coordinates of the bottom left corner of the screen plus half the width and height of the duck sprite plus a random number between 0 and the width and height of the screen minus the width and height of the duck sprite
        float screenX5 = screenX3 + Random.Range(0, screenWidth3);
        float screenY5 = screenY3 + Random.Range(0, screenHeight3);

        // return chosen position
        return new Vector3(screenX5, screenY5, 0);

    }

}
