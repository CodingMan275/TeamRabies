using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject Res;
    public SaveData SD;
    public ShopData EShop;
    public ShopData SShop;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name.CompareTo("MainMenu") == 0)
            if(SD.Round > 1 && Res != null && !SD.Dead)
                Res.SetActive(true);


        if (SceneManager.GetActiveScene().name.CompareTo("Defeat") == 0 && SD.lastStand)
                Res.SetActive(true);

    }

    public void ChangeScene(string s)
    {
        SD.UD.inShop = s.CompareTo("Shop") == 0 ? true : false;
        if(SD.UD.inShop)
        {
            SD.CopyValues(true);
            EShop.CopyValues(true);
            SShop.CopyValues(true);
        }
        SceneManager.LoadSceneAsync(s);
    }

    public void Resume()
    {
        SD.UD.inShop = true;
        SD.CopyValues(false);
        EShop.CopyValues(false);
        SShop.CopyValues(false);
        SceneManager.LoadScene("Shop");
    }

    public void Revive()
    {
        SD.Revive();
        Resume();
    }

    public void MainMenu()
    {
        SD.UD.inShop = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void Arena1()
    {
        SD.Reset();
        EShop.Reset();
        SShop.Reset();
        SD.CopyValues(true);
        EShop.CopyValues(true);
        SShop.CopyValues(true);

        SceneManager.LoadSceneAsync("Arena 1-1");
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
