using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public SaveData SD;
    public void StartGame()
    {
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

    public void Shop()
    {
        SceneManager.LoadScene("shop");
    }

    public void Arena1()
    {
        SD.Reset();
        SceneManager.LoadScene(5);
    }
    public void Arena2()
    {
        SceneManager.LoadScene(6);
    }
    public void Arena3()
    {
        SceneManager.LoadScene(7);
    }
    public void Arena4()
    {
        SceneManager.LoadScene(8);
    }
    public void Arena5()
    {
        SceneManager.LoadScene(9);
    }

    public void Exit()
    {
            Application.Quit(); 
    }

    public void Bug()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdfKdaVEU_FZnzPAYdUxznZTS5U-tKyvJvWXCvKqxZBWj9O3A/viewform?usp=header");
    }

}
