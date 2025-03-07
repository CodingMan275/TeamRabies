using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SaveData/PlayerSave")]
public class SaveData : ScriptableObject
{
    public int killCount = 0;
    public int Scrap = 0;

    //Player stats to be modified
    public double FireRateMod = 1;
    public double DodgeCooldownMod = 1;
    public double ScrapDropMod = 1;

    public bool explodingBullets = false;

    //Player abilities
    //Player adds the abilities that are marked true on enter
    //Dash

    public List<Upgrade> UpgradePoolRound1;
    public List<Upgrade> UpgradePoolRound2;
    public List<Upgrade> UpgradePoolRound3;
    public List<Upgrade> UpgradePoolRound4;
    public List<Upgrade> UpgradePoolRound5;

    public int Round = 1;

}
