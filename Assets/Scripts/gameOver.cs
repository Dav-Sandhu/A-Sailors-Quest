using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    private float timer;

    void Start()
    {
        timer = 3f;    
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) 
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
