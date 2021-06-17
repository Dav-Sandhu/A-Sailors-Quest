using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTracker : MonoBehaviour
{

    public GameObject player;
    public GameObject currentGround;
    public GameObject nextGround;

    public GameObject finishZonePrefab;
    public GameObject normalGroundPrefab;
    public GameObject doubleGapPrefab;
    public GameObject raisedGroundPrefab;
    public GameObject raisedGroundGapPrefab;
    public GameObject loweredGroundPrefab;
    public GameObject loweredGroundGapPrefab;

    public GameObject smallPlatform;
    public GameObject largePlatform;

    public GameObject coinPrefab;

    public GameObject[] things;

    public int spawned;

    public string currentLevel;

    public zone currentZoneScript;
    public Player pScript;

    public List<GameObject> platforms = new List<GameObject>();


    void Start()
    {
        spawned = 0;
        player = GameObject.Find("Player");
        pScript = player.GetComponent<Player>();
        currentLevel = pScript.currentLevel;
        GameObject firstGround = Instantiate(normalGroundPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        currentGround = firstGround;
    }

    void Update()
    {
        currentZoneScript = currentGround.GetComponent<zone>();
        if (player.transform.position.x - currentGround.transform.position.x > 0 && !currentZoneScript.usedToSpawn && spawned < 11)
        {

            transform.Translate(new Vector3(40.0f, 0f, 0f));


            int whichSpawned = Random.Range(0, 6);
            GameObject temp = normalGroundPrefab;

            if (whichSpawned == 1)
                temp = doubleGapPrefab;
            if (whichSpawned == 2)
                temp = raisedGroundPrefab;
            if (whichSpawned == 3)
                temp = loweredGroundPrefab;
            if (whichSpawned == 4)
                temp = loweredGroundGapPrefab;
            if (whichSpawned == 5)
                temp = raisedGroundGapPrefab;


            if (spawned == 10)
                nextGround = Instantiate(finishZonePrefab, new Vector3(transform.position.x, normalGroundPrefab.transform.position.y, 0), Quaternion.identity);
            else
            {
                nextGround = Instantiate(temp, new Vector3(transform.position.x, temp.transform.position.y, 0), Quaternion.identity);
                spawned++;

                int numOfPlats = Random.Range(0, 3);
                float platxpos = 0;
                float platypos = 0;
                int ymod = 6;
                if (whichSpawned == 2 || whichSpawned == 5)
                    ymod = 11;
                for (int i = 0; i < numOfPlats; i++)
                {
                    if (numOfPlats == 1)
                        platxpos = Random.Range(transform.position.x - 15, transform.position.x + 15);
                    else if (numOfPlats == 2)
                    {
                        if (i == 0)
                            platxpos = Random.Range(transform.position.x - 13, transform.position.x - 2);
                        else
                            platxpos = Random.Range(transform.position.x + 2, transform.position.x + 13);
                    }



                    platypos = transform.position.y + ymod;

                    GameObject tempPlat = new GameObject();
                    int whichPlat = Random.Range(0, 2);
                    if (whichPlat == 0)
                        tempPlat = Instantiate(smallPlatform, new Vector3(platxpos, platypos, 0), Quaternion.identity);
                    else if (whichPlat == 1)
                        tempPlat = Instantiate(largePlatform, new Vector3(platxpos, platypos, 0), Quaternion.identity);

                    tempPlat.transform.parent = nextGround.transform;
                    platforms.Add(tempPlat);
                }  
            }
            currentZoneScript.usedToSpawn = true;
        }


        //everytime i do something like "GameObject name = new GameObject()" for a temporary
        //placeholder it makes an empty game object, this just cleans those up
        foreach(GameObject garbage in Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (garbage.name == "New Game Object")
                Destroy(garbage);
        }
    }
}
