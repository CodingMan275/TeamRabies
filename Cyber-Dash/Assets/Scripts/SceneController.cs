using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartGame()
    {
        print("Hello");
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Victory()
    {
        SceneManager.LoadScene(2);
    }

    public void Defeat()
    {
        SceneManager.LoadScene(3);
    }

}
