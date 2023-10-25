using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleBehavior : MonoBehaviour
{
    public GameObject gmObject;
    private GameManagerBehavior gm;

    // Start is called before the first frame update
    void Start()
    {
        // hide the mouse cursor
        Cursor.visible = false;
        gm = gmObject.GetComponent<GameManagerBehavior>();
        // start the coroutine to shake the reticle
        StartCoroutine(mouseManager());

    }

    // Update is called once per frame
    void Update()
    {
        // keep the sprite centered on the mouse
        
        // get the mouse position
        Vector3 mousePos = Input.mousePosition;
        // convert the mouse position to world coordinates
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
        // set the z position to 0
        mousePosWorld.z = 1;
        // set the position of the reticle to the mouse position
        transform.position = mousePosWorld;
        transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

    }

    IEnumerator mouseManager()
    {
        float currentAngle = 0;
        while (true)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
            mousePosWorld.z = 1;
            Vector3 offset = new Vector3(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle), 0) * 0.1f;
            transform.position = mousePosWorld + offset;

        }

    }


}
