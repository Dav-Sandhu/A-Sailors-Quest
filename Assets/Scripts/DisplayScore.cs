using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DisplayScore : MonoBehaviour
{
    string filePath;
    string score;

    void Start()
    {
        filePath = "Assets/Files/scores.csv";

        if (!File.Exists(filePath))
        {
            string[] createText = { "0" };
            File.WriteAllLines(filePath, createText);
        }

        string[] fileLines = File.ReadAllLines(filePath);

        foreach (string line in fileLines)
        {
            score = line;
        }
        Text text = gameObject.AddComponent<Text>();
        text.text = "Total Score: " + score;

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = ArialFont;
        text.fontSize = 30;
        text.alignment = TextAnchor.MiddleCenter;
        text.material = ArialFont.material;
    }
}
