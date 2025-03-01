using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Logic : MonoBehaviour
{
    /*
    public static int RapidFireCircuitCost = 1;
    public enum Upgrades
    { 
    RapidFireCiruit
    }

    public static int GetCost(Upgrades upgrade)
    {
        switch (upgrade)
        {
            case Upgrades.RapidFireCiruit: return RapidFireCircuitCost;
        }
        return 0;
    }


    public static string GetName(Upgrades upgrade)
    {
        switch (upgrade)
        {
            case Upgrades.RapidFireCiruit: return "Rapid Fire Circuit";
        }
        return "";
    }
    */

    // Start is called before the first frame update


    public GameObject scrapDisplay;
    public SaveData player;
    public Transform shopUiTransform;
    public GameObject UpgradePrefab;

    public Upgrade[] Upgrades;

    void Start()
    {
        foreach (Upgrade upgrade in Upgrades)
        {
            GameObject item = Instantiate(UpgradePrefab, shopUiTransform);

            upgrade.itemRef = item;

            foreach (Transform child in item.transform)
            {
                if (child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap Cost: " + upgrade.Cost;
                }
                else if (child.gameObject.name == "Name")
                {
                    child.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = upgrade.Name;
                }
                else if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = upgrade.Sprite;
                }
            }

            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuyUpgrade(upgrade);
            });
        }
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if (player.Scrap >= upgrade.Cost)
        {
            player.Scrap -= upgrade.Cost;
            scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap " + player.Scrap;

            ApplyUpgrade(upgrade);
        }
    }


    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.Name)
        {
            case "Rapid Fire Circuit": player.FireRateMod *= .85;
                break;
            case "Dodge Booster": player.DodgeCooldownMod *= .75;
                break;
            default:
                Debug.Log("What did you just do");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
       scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap " + player.Scrap;
    }


}

[System.Serializable]
public class Upgrade
{
    public string Name;
    public int Cost;
    public Sprite Sprite;
    [HideInInspector] public GameObject itemRef;
}
