using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SaveData/ShopSave")]
public class ShopData : ScriptableObject
{
    //List of energy upgrades that change and move from rounds
    public List<Upgrade> PoolRound1;
    public List<Upgrade> PoolRound2;
    public List<Upgrade> PoolRound3;
    public List<Upgrade> PoolRound4;
    public List<Upgrade> PoolRound5;

    //Base energy upgrades of each round
    public List<Upgrade> StaticPoolRound1;
    public List<Upgrade> StaticPoolRound2;
    public List<Upgrade> StaticPoolRound3;
    public List<Upgrade> StaticPoolRound4;
    public List<Upgrade> StaticPoolRound5;
}
