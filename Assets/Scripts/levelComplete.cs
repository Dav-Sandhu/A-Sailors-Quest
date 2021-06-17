using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelComplete : MonoBehaviour
{
    private int level;
    private float timer;
    public string currentLevel;

    void Start()
    {
        
        timer = 3f;    
    }

    void Update()
    {
        
        timer -= Time.deltaTime;
        /*
        if (timer <= 0) 
        {
            if (pScript.currentLevel == "Level 1")
                SceneManager.LoadScene("Level 2");
            else if (pScript.currentLevel == "Level 2")
                SceneManager.LoadScene("Level 3");
            else if (pScript.currentLevel == "Level 3")
                SceneManager.LoadScene("Main Menu");
        }
        */
    }
}
