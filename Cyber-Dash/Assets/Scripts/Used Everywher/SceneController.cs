using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public SaveData SD;
    public void ChangeScene(string s)
    {
        SD.UD.inShop = s.CompareTo("Shop") == 0 ? true : false;

        SceneManager.LoadScene(s);
    }

    public void MainMenu()
    {
        SD.UD.inShop = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void Arena1()
    {
        SD.Reset();
        SceneManager.LoadScene("Arena 1-1");
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
