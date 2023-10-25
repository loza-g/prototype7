using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject ui;
    
    private TextMeshProUGUI ducksRemainingText;
    private TextMeshProUGUI bulletsRemainingText;
    private TextMeshProUGUI timeRemainingText;
    [SerializeField] private AudioSource audioPlayerWin;
    [SerializeField] private AudioSource audioPlayerLose;
    public int ducksRemaining = 10;
    public int bulletsRemaining = 10;
    public float totalTime = 30f;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        ducksRemainingText = ui.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        bulletsRemainingText = ui.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        timeRemainingText = ui.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

        timeLeft = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            timeLeft = 0;

            if(ducksRemaining > 0)
            {
                //reload scene, play lose audio
                RestartCurrentScene();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        ducksRemainingText.text = "ducks remaining: " + ducksRemaining;
        bulletsRemainingText.text = "bullets remaining: " + bulletsRemaining;
        timeRemainingText.text = "time remaining: " + Mathf.RoundToInt(timeLeft);

        // check if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletsRemaining > 0)
            {
                Debug.Log("mouse down");
                // get the mouse position
                Vector3 mousePos = Input.mousePosition;
                // convert the mouse position to world coordinates
                Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
                // set the z position to 0
                mousePosWorld.z = 0;

                // check if a duck is at the mouse position
                // get the collider at the mouse position
                Collider2D hitCollider = Physics2D.OverlapPoint(mousePosWorld);

                Debug.Log(hitCollider);

                // if the collider is not null
                if (hitCollider != null)
                {
                    // if the collider is a duck
                    if (hitCollider.gameObject.tag == "Duck")
                    {
                        // destroy the duck
                        Destroy(hitCollider.gameObject);
                        // decrease the number of ducks remaining
                        ducksRemaining--;
                    }
                    if(hitCollider.gameObject.tag == "bulletEater" || hitCollider.gameObject.tag == "duckSpeed" ||
                        hitCollider.gameObject.tag == "duckTeleport" || hitCollider.gameObject.tag == "reticleSwap")
                    {
                        Debug.Log("Shot power up.");
                        if(hitCollider.gameObject != null)
                        {
                            Destroy(hitCollider.gameObject);
                        }
                    }
                }

                bulletsRemaining--;
            }
        }

        if(ducksRemaining == 0)
        {
            //load next scene, play success audio 
            LoadNextScene();
        }
        if(bulletsRemaining == 0)
        {
            RestartCurrentScene();
        }

    }

    public int GetBulletCount()
    {
        return bulletsRemaining;
    }

    public void UpdateBulletCount(int num)
    {
        bulletsRemaining -= num;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
