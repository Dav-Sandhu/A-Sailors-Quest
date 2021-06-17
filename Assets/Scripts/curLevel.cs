using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class curLevel : MonoBehaviour
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
        Scene scene = SceneManager.GetActiveScene();
        text.text = scene.name + "\n" + "Health: " + player.GetComponent<Player>().health.ToString();

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        text.fontSize = 20;
        text.alignment = TextAnchor.MiddleCenter;
        text.material = ArialFont.material;
    }
}
