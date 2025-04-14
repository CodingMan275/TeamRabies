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


    public void ChangeScene(string s)
    {
        if (SD.DoubleDamage)
            StartCoroutine(SD.ForceUltReset());
        SceneManager.LoadScene(s);
    }

    public void MainMenu()
    {
        if (SD.DoubleDamage)
            StartCoroutine(SD.ForceUltReset());
        SceneManager.LoadScene(0);
    }

    public void Arena1()
    {
        SD.Reset();
        SceneManager.LoadScene(5);
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
