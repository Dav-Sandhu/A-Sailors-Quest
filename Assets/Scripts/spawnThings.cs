using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnThings : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject healthPowerup;
    public GameObject invisiblePowerup;
    public GameObject speedPowerup;
    public GameObject teleportPowerup;
    public GameObject laserPowerup;

    
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject boss;


    public List<GameObject> pickupList = new List<GameObject>();
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> bossLocation = new List<GameObject>();

  
    public void spawn(string currentLevel, int zoneNum, GameObject parent)
    {
        
        foreach (Transform child in parent.transform)
        {
            if (child.tag == "spawn")
                pickupList.Add(child.gameObject);

            if (child.tag == "enemySpawn")
                enemyList.Add(child.gameObject);
            if (child.tag == "bossSpawn")
                bossLocation.Add(child.gameObject);
        }

        foreach (GameObject spawnPoint in pickupList)
        {
            //changing what spawns depending on level
            int spawnPowerup = 0;
            if (currentLevel == "Level 1")
                spawnPowerup = Random.Range(0, 20);
            else if (currentLevel == "Level 2")
                spawnPowerup = Random.Range(20, 40);
            else if (currentLevel == "Level 3")
                spawnPowerup = Random.Range(0, 50);
            
            switch (spawnPowerup)
            {
                case 0:
                    Instantiate(healthPowerup, spawnPoint.transform);
                    break;
                case 10:
                    Instantiate(invisiblePowerup, spawnPoint.transform);
                    break;
                case 20:
                    Instantiate(teleportPowerup, spawnPoint.transform);
                    break;
                case 30:
                    Instantiate(laserPowerup, spawnPoint.transform);
                    break;
                case 40:
                    Instantiate(speedPowerup, spawnPoint.transform);
                    break;
                default:
                    Instantiate(coinPrefab, spawnPoint.transform);
                    break;
            }
            
            
        }
        pickupList.Clear();

        if (zoneNum < 10)
        {
            foreach (GameObject spawnPoint in enemyList)
            {
                //changing what spawns depending on level
                int enemyType = 0;
                if (currentLevel == "Level 1")
                    enemyType = Random.Range(0, 2);
                else if (currentLevel == "Level 2")
                    enemyType = Random.Range(2, 4);
                else if (currentLevel == "Level 3")
                    enemyType = Random.Range(0, 5);

                GameObject tempEnemy = new GameObject();
                switch (enemyType)
                {
                    case 0:
                        tempEnemy = enemy1;
                        break;
                    case 1:
                        tempEnemy = enemy2;
                        break;
                    case 2:
                        tempEnemy = enemy3;
                        break;
                    case 3:
                        tempEnemy = enemy4;
                        break;
                    case 4:
                        tempEnemy = enemy5;
                        break;
                }

                Instantiate(tempEnemy, spawnPoint.transform);
            }
        }

        if(zoneNum == 10 && parent.tag != "platform")
        {
            foreach (GameObject spawnPoint in bossLocation)
                Instantiate(boss, spawnPoint.transform);
        }

        enemyList.Clear();
        bossLocation.Clear();
    }
}