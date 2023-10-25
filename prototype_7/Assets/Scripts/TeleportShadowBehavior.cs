using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportShadowBehavior : MonoBehaviour
{
    public float teleportDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitXSecondsThenDestroy(teleportDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitXSecondsThenDestroy(float s)
    {
        yield return new WaitForSeconds(s);
        Destroy(gameObject);
    }
}
