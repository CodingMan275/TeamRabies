using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public SaveData SD;
    public void StartGame()
    {
        print("Hello");
        SD.Scrap = 0;
        SD.Energy = 0;
        SD.Round = 1;
        SD.killCount = 0;
        SD.explodingBullets = false;
        SD.Pistol = true;
        SD.FireRateMod = 1;
        SD.DodgeCooldownMod = 1;
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

}
