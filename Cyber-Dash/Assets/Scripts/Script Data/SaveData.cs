using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SaveData/PlayerSave")]
public class SaveData : ScriptableObject
{
    [Header("Player Stats")]
    public int Round = 1;
    public int killCount = 0;
    public int Scrap = 0;
    public int Energy = 0;

    [Header("Mod Player")]
    //Player stats to be modified
    public float FireRateMod = 0;
    public double DodgeCooldownMod = 1;
    public double ScrapDropMod = 1;
    public bool explodingBullets = false;

    [Header("Player Abilities")]
    //Player abilities
    //Player adds the abilities that are marked true on enter
    public bool Turret = false;

    [Header("Weapons")]
    //Weapons
    public bool Pistol = true;
    public bool Bolt_Launcher;
    public bool Shotgun;
    public bool PlasmaCutter;

    [Header("Weapon Mods")]
    public float PistolFireRateMod = 1;
    public float ShotgunFireRateMod = 1;
    public float BoltLauncherFireRateMod = 1;
    public float BeamFireRateMod = 1;



    [Header("Smelly Stinky List")]
    //List of energy upgrades that change and move from rounds
    public List<Upgrade> EnergyPoolRound1;
    public List<Upgrade> EnergyPoolRound2;
    public List<Upgrade> EnergyPoolRound3;
    public List<Upgrade> EnergyPoolRound4;
    public List<Upgrade> EnergyPoolRound5;
    
    //Base energy upgrades of each round
    public List<Upgrade> StaticEnergyPoolRound1;
    public List<Upgrade> StaticEnergyPoolRound2;
    public List<Upgrade> StaticEnergyPoolRound3;
    public List<Upgrade> StaticEnergyPoolRound4;
    public List<Upgrade> StaticEnergyPoolRound5;

    //List of energy upgrades that change and move from rounds
    public List<Upgrade> ScrapPoolRound1;
    public List<Upgrade> ScrapPoolRound2;
    public List<Upgrade> ScrapPoolRound3;
    public List<Upgrade> ScrapPoolRound4;
    public List<Upgrade> ScrapPoolRound5;

    //Base energy upgrades of each round
    public List<Upgrade> StaticScrapPoolRound1;
    public List<Upgrade> StaticScrapPoolRound2;
    public List<Upgrade> StaticScrapPoolRound3;
    public List<Upgrade> StaticScrapPoolRound4;
    public List<Upgrade> StaticScrapPoolRound5;



}
