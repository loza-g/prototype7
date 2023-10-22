using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour
{
    public GameObject ui;

    private TextMeshProUGUI ducksRemainingText;
    private TextMeshProUGUI bulletsRemainingText;

    public int ducksRemaining = 10;
    public int bulletsRemaining = 10;

    // Start is called before the first frame update
    void Start()
    {
        ducksRemainingText = ui.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        bulletsRemainingText = ui.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ducksRemainingText.text = "ducks remaining: " + ducksRemaining;
        bulletsRemainingText.text = "bullets remaining: " + bulletsRemaining;

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
                }

                bulletsRemaining--;
            }
        }
    }

}
