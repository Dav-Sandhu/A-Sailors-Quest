using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class curScore : MonoBehaviour
{
    public Transform player;
    Text text;

    void Start()
    {
        text = gameObject.AddComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        text.text = "Coins Collected: " + player.GetComponent<Player>().points.ToString() + "\n" 
            + "Enemies Killed: " + player.GetComponent<Player>().enemiesKilled.ToString();

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        text.fontSize = 20;
        text.alignment = TextAnchor.MiddleCenter;
        text.material = ArialFont.material;
    }
}
