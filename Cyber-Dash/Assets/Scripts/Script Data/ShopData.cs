using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SaveData/ShopSave")]
public class ShopData : ScriptableObject
{
    public ShopData SaveShop;

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


    // This copies the junk
    public void CopyValues(bool t)
    {
        ShopData source = t ? this : SaveShop;
        ShopData target = !t ? this : SaveShop;

        FieldInfo[] fields = typeof(ShopData).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(List<Upgrade>))
                field.SetValue(target, new List<Upgrade>((IEnumerable<Upgrade>)field.GetValue(source)));
            else
                field.SetValue(target, field.GetValue(source)); // Copy value
        }

    }

    public void Reset()
    {
        PoolRound1 = StaticPoolRound1;
        PoolRound2 = StaticPoolRound2;
        PoolRound3 = StaticPoolRound3;
        PoolRound4 = StaticPoolRound4;
        PoolRound5 = StaticPoolRound5;
    }
}
