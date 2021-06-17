using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CollectingCoins : MonoBehaviour
{
    public GameObject Object;
    string filePath;

    void Start() 
    {
        filePath = "Assets/Files/scores.csv";

        if (!File.Exists(filePath))
        {
            string[] createText = { "0" };
            File.WriteAllLines(filePath, createText);
        }
    }

    public void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("coin");
            collisionInfo.gameObject.GetComponent<Player>().points++;

            string[] fileLines = File.ReadAllLines(filePath);

            foreach (string line in fileLines) 
            {
                int temp = int.Parse(line) + 1;


                File.WriteAllText(filePath, temp.ToString()); 
            }

            Destroy(Object);
        }
    }

    void Update()
    {
        transform.Rotate(0, 85 * Time.deltaTime, 0);

    }
}
