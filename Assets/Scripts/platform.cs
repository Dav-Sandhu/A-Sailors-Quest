using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public GameObject Tracker;
    public ZoneTracker zScript;
    public int zoneNum;

    public bool spawnedThings;

    public spawnThings spawner;
    public destroyThings destroyer;

    void Start()
    {
        Tracker = GameObject.Find("Tracker");
        zScript = Tracker.GetComponent<ZoneTracker>();
        zoneNum = zScript.spawned;

        spawnedThings = false;
    }

    void Update()
    {
        if (!spawnedThings)
        { 
            spawner.spawn(zScript.currentLevel, zoneNum, this.gameObject);
            spawnedThings = true;
        }
    }
}
