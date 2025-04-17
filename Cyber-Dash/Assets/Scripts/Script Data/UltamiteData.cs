using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SaveData/UltStuff")]
public class UltamiteData : ScriptableObject
{
    [Header("Weapon Modifers")]
    public float BeamRateMod = 2;
    public float PistolRateMod = 2;
    public float BoltRateMod = 2;
    public float ShotgunTimerMod = 1;
    public float WalkRateMod = 2;
    public float RunRateMod = 2;

    [Header("Ult Version")]
    public float BeamRate;
    public float PistolRate;
    public float BoltRate;
    public float ShotGunRate;
    public float WalkFast;
    public float RunFast;

    [Header("Reset me to Reset stuff")]
    [Range(0, 100)] public int UltPerc = 0;
    public bool DoubleDamage = false;
    public bool inShop = false;
    public float UltDur = 0;
    public float UltCoolDown = 0;


    [Header("Timers")]
    public float HealBuff;
    public float UltTimer = 10f;
    public float UltUseage = 30f;



    public void Reset()
    {
        inShop = false;
        UltDur = 0;
        UltCoolDown = 0;
        UltPerc = 0;
        DoubleDamage = false;
    }

    // Start is called before the first frame update
    public void ChangeUltVars(SaveData SD)
    {
        if (UltDur <= 0 && UltCoolDown <= 0)
            DoubleDamage = false;

        BeamRate = SD.beamCharge * BeamRateMod;
        PistolRate = SD.PistolFireRateMod * PistolRateMod;
        BoltRate = SD.BoltLauncherFireRateMod * BoltRateMod;
        ShotGunRate = SD.ShotgunReloadTime - ShotgunTimerMod;
        WalkFast = SD.WalkSpeed * WalkRateMod;
        RunFast = SD.RunSpeed * RunRateMod;

    }
}
