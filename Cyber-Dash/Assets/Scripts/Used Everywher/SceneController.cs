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
        SceneManager.LoadScene(4);
    }

    public void Arena1()
    {
        print("Hello");
        SD.Scrap = 0;
        SD.Energy = 0;
        SD.Round = 1;
        SD.killCount = 0;
        SD.Turret = false;
        SD.explodingBullets = false;
        SD.Pistol = true;
        SD.FireRateMod = 1;
        SD.DodgeCooldownMod = 1;
        SD.curWep = new Upgrade("Pistol", 0, Resources.Load<Sprite>("ALFE_Art/gun 1"), "A basic Pistol", false);
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

}
