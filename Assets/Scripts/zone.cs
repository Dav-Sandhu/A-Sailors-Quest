using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour
{
    public GameObject Tracker;
    public GameObject coinPrefab;

    public ZoneTracker zScript;
    public spawnThings spawner;
    public destroyThings destroyer;

    public bool usedToSpawn;
    public bool spawnedThings;
    public int zoneNum;
    

    void Start()
    {
        Tracker = GameObject.Find("Tracker");
        zScript = Tracker.GetComponent<ZoneTracker>();
        zoneNum = zScript.spawned;
        usedToSpawn = false;
        spawnedThings = false;
        if (zScript.spawned == 0 || zScript.spawned == 11)
            spawnedThings = true;
        
    }

    void Update()
    {
        if (!spawnedThings)
        {
            spawner.spawn(zScript.currentLevel, zoneNum, this.gameObject);
            spawnedThings = true;
        }
        destroyer.destroy(this.gameObject);
    }
}
