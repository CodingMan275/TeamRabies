using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName ="SaveData/PlayerSave")]
public class SaveData : ScriptableObject
{
    [Header("Player Stats")]
    public int Round = 1;
    public int killCount = 0;
    public int Scrap = 0;
    public int Energy = 0;
    public int MaxHealth = 40;
    public int Health = 40;
    [Range(0,100)]public int UltPerc = 0;
    public float WalkSpeed = 15f;
    public float RunSpeed = 20f;

    [Header("Mod Player")]
    //Player stats to be modified
    public float FireRateMod = 0;
    public double DodgeCooldownMod = 0;
    public double ScrapDropMod = 1;
    public bool explodingBullets = false;
    public bool criticalStrike = false;
    public bool scrapRecycle = false;
    public int healthBuff = 0;
    public bool energyDeflector = false;
    public bool adaptiveArmor = false;

    [Header("Player Abilities")]
    //Player abilities
    //Player adds the abilities that are marked true on enter
    public bool Turret = false;
    public bool repairPack = false;
    public bool overchargeBattery = false;

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
    public int HowManyPierce = 0;
    public float ShotgunReloadTime = 2;
    public float ShotGunAmmo = 2;
    public int boltIncreaseDamage = 0;
    public float boltSpeedIncrease = 0;
    public float beamCharge = .01f;
    public int beamDamage = 5;
    public bool DoubleDamage;



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

    public Sprite gun;
    public Upgrade curWep;
    public List<Upgrade> PastWeapons;

    public void Reset()
    {

       curWep = new Upgrade("Pistol", 0, gun, gun, false);

        Round = 1;
         killCount = 0;
        Scrap = 0;
        Energy = 0;
        Health = MaxHealth;
        UltPerc = 0;
        WalkSpeed = 15f;
        RunSpeed = 20;
        DoubleDamage = false;
    //Player stats to be modified
    FireRateMod = 1;
    DodgeCooldownMod = 0;
    ScrapDropMod = 1;
   explodingBullets = false;
        criticalStrike = false;
        scrapRecycle = false;
        healthBuff = 0;
        energyDeflector = false;
        adaptiveArmor = false;


        //Player abilities
        //Player adds the abilities that are marked true on enter
        Turret = false;
        repairPack = false;
        overchargeBattery = false;


        //Weapons
        Pistol = true;
    Bolt_Launcher = false;
    Shotgun = false;
    PlasmaCutter= false;


   PistolFireRateMod = 1;
   ShotgunFireRateMod = 1;
        ShotgunReloadTime = 2;
   BoltLauncherFireRateMod = 1;
  BeamFireRateMod = 1;
  HowManyPierce = 0;
        boltIncreaseDamage = 0;
        boltSpeedIncrease = 0;
        beamCharge = .01f;
        beamDamage = 5;
    }

    public IEnumerator StatBoost()
    {
        BeamFireRateMod /= 2;
        PistolFireRateMod /= 2;
        BoltLauncherFireRateMod /= 2;
        WalkSpeed *= 2;
        RunSpeed *= 2;
        Health += 20;
        DoubleDamage = true;
        yield return new WaitForSeconds(10);
        ForceUltReset();
        
    }

    public IEnumerator ForceUltReset()
    {
        BeamFireRateMod *= 2;
        PistolFireRateMod *= 2;
        BoltLauncherFireRateMod *= 2;
        WalkSpeed /= 2;
        RunSpeed /= 2;
        Health -= 20;
        DoubleDamage = false;
        yield return new WaitForSeconds(0.5f);
    }

}
