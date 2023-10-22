using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // hide the mouse cursor
        Cursor.visible = false;
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
        mousePosWorld.z = 0;
        // set the position of the reticle to the mouse position
        transform.position = mousePosWorld;

    }
}
