using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Pause() 
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else { Time.timeScale = 1; }
    }
}
