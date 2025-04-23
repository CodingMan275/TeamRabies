using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName ="SaveData/PlayerSave")]
public class SaveData : ScriptableObject
{
    public UltamiteData UD;
    public SaveData SavePlayer;

    [Header("Player Stats")]
    public int Round = 1;
    public int killCount = 0;
    public int Scrap = 0;
    public int Energy = 0;
    public int MaxHealth = 40;
    public int Health = 40;
    [Range(0,45)]public int ExtraHealth = 0;  // 20 from ult + 25 from shield Buff
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
    public bool lastStand = false;

    [Header("Player Abilities")]
    //Player abilities
    //Player adds the abilities that are marked true on enter
    public bool Turret = false;
    public bool repairPack = false;
    public bool overchargeBattery = false;
    public bool injector = false;

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
    public int HowManyPierce = 0;
    public float ShotgunReloadTime = 2;
    public float ShotGunAmmo = 2;
    public int boltIncreaseDamage = 0;
    public float boltSpeedIncrease = 0;
    public float beamCharge = .01f;
    public int beamDamage = 5;


    public Sprite gun;
    public Upgrade curWep;
    public List<Upgrade> PastWeapons;

    public void Reset()
    {

       curWep = new Upgrade("Pistol", 0, gun, gun,"", false);

        Round = 1;
         killCount = 0;
        Scrap = 0;
        Energy = 0;
        Health = MaxHealth;
        ExtraHealth = 0;
        WalkSpeed = 15f;
        RunSpeed = 20;
        UD.Reset();
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
        lastStand = false;


        //Player abilities
        //Player adds the abilities that are marked true on enter
        Turret = false;
        repairPack = false;
        overchargeBattery = false;
        injector = false;



        //Weapons
        Pistol = true;
    Bolt_Launcher = false;
    Shotgun = false;
    PlasmaCutter= false;


   PistolFireRateMod = 1;
   ShotgunFireRateMod = 1;
        ShotgunReloadTime = 2;
   BoltLauncherFireRateMod = 1;
  HowManyPierce = 0;
        boltIncreaseDamage = 0;
        boltSpeedIncrease = 0;
        beamCharge = .01f;
        beamDamage = 5;
    }

    // This copies the junk
    public void CopyValues(bool t)
    {
            UD.CopyValues(t);
            SaveData source = t ? this : SavePlayer;
            SaveData target = !t ? this : SavePlayer;

        FieldInfo[] fields = typeof(SaveData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                field.SetValue(target, field.GetValue(source)); // Copy value
            }

            Debug.Log("Copied values from ObjectA to ObjectB!");
        
    }






}
