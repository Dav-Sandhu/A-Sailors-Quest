using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyThings : MonoBehaviour
{
    Renderer myRen;
    private Vector3 outOfScreen;

    public GameObject Tracker;
    public ZoneTracker zScript;

    void Start()
    {
        myRen = GetComponent<Renderer>();
    }

    public void destroy(GameObject gameObject)
    {
        Tracker = GameObject.Find("Tracker");
        zScript = Tracker.GetComponent<ZoneTracker>();

        outOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (gameObject.transform.position.x < outOfScreen.x - 25)
        {
            foreach (Transform child in gameObject.transform) {
                if (child.tag == "platform")
                    zScript.platforms.Remove(child.gameObject); 
            }

            Destroy(gameObject);
        }
    }
}