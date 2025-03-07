using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Logic : MonoBehaviour
{

    // Start is called before the first frame update

    public int HowManyItemsInShop = 4;

    public GameObject scrapDisplay;
    public SaveData player;
    public Transform shopUiTransform;
    public GameObject UpgradePrefab;
    public Weapon_Controller WPC;

    //Master list of all perks
    public List<Upgrade> UpgradesList;

    //Current pool of upgrades
    List<Upgrade> pool;





    void Start()
    {

        //The list of which items are currently in the pool
        List<Upgrade> CopyList = new List<Upgrade>(FindPool(player.Round));

        //Iterate through each random perk
        //foreach (Upgrade upgrade in UpgradesList)
        for(int i = 0; i < HowManyItemsInShop ;i++)
        {

            int randnumpicked =  Random.Range(0, CopyList.Count);

            Upgrade upgrade = CopyList[randnumpicked];

            CopyList.RemoveAt(randnumpicked);
            //player.UpgradePoolRound1.RemoveAt(randnumpicked);

            //Actually create it
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
                BuyUpgrade(upgrade, randnumpicked);
            });
        }
    }

    

    public void BuyUpgrade(Upgrade upgrade, int randnum)
    {
        if (player.Scrap >= upgrade.Cost)
        {
            player.Scrap -= upgrade.Cost;
            scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap " + player.Scrap;

            //These are for 1 time purchases
            FindPool(player.Round)[randnum].Purchased = true;
            upgrade.itemRef.SetActive(false);

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
            case "Bolt Launcher": 
                WPC.AssignWeapon("Bolt Launcher");
                break;
            case "Explosive Rounds":
                player.explodingBullets = true;
                break;
            default:
                Debug.Log("What did you just do");
                //Example code
                //List<Upgrade> ListRef = FindPool
                //List.Ref.Add(new Upgrade(SwagPerk, 5, SwagIcon.png))

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

    List<Upgrade> FindPool(int round)
    {
        switch (round)
        {
            case 1:
                return player.UpgradePoolRound1;
            case 2:
                return player.UpgradePoolRound2;
            case 3:
                return player.UpgradePoolRound3;
            case 4:
                return player.UpgradePoolRound4;
            case 5:
                return player.UpgradePoolRound5;

        }
        return UpgradesList;
    }

    void EndShopUpdate()
    {
        List<Upgrade> currentPool = FindPool(player.Round);
        List<Upgrade> nextPool = FindPool(player.Round + 1);

        for (int i = 0; i < currentPool.Count; i++)
        {
            if (!currentPool[i].Purchased)
            {
                nextPool.Add(currentPool[i]);
            }
        }

    }
}

[System.Serializable]
public class Upgrade
{
    public string Name;
    public int Cost;
    public Sprite Sprite;
    [HideInInspector] public GameObject itemRef;
    [HideInInspector] public bool Purchased;
}
