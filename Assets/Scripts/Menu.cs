using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void level1() 
    {
        SceneManager.LoadScene("Level 1");
    }

    public void level2() 
    {
        SceneManager.LoadScene("Level 2");
    }

    public void level3() 
    {
        SceneManager.LoadScene("Level 3");
    }

    public void description() 
    {
        SceneManager.LoadScene("Description");
    }

    public void menu() 
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void quit() 
    {
        Application.Quit();
    }
}
