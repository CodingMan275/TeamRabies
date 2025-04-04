using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrap_Shop_Logic : MonoBehaviour
{

    // Start is called before the first frame update

    public int HowManyItemsInShop = 4;

    public GameObject scrapDisplay;
    public SaveData player;
    public Transform shopUiTransform;
    public GameObject UpgradePrefab;
    public Weapon_Controller WPC;
    public Shop_Interactiob SB;

    public Sprite gunSprite;

    //Master list of all perks
    public List<Upgrade> UpgradesList;

    public GameObject weaponsChest;

    void Start()
    {

        //Reset perk pool
        if(player.Round == 1)
        {
            player.curWep = new Upgrade("Pistol", 0, Resources.Load<Sprite>("ALFE_Art/gun 1"), "A basic Pistol, false", false);
            player.PastWeapons.Clear();
            player.ScrapPoolRound1 = new List<Upgrade>(player.StaticScrapPoolRound1);
            player.ScrapPoolRound2 = new List<Upgrade>(player.StaticScrapPoolRound2);
            player.ScrapPoolRound3 = new List<Upgrade>(player.StaticScrapPoolRound3);
            player.ScrapPoolRound4 = new List<Upgrade>(player.StaticScrapPoolRound4);
            player.ScrapPoolRound5 = new List<Upgrade>(player.StaticScrapPoolRound5);
        }

        //The list of which items are currently in the pool
      //  CopyList = new List<Upgrade>(FindPool(player.Round));


        //Iterate through each random perk
        //foreach (Upgrade upgrade in UpgradesList)
        for(int i = 0; i < HowManyItemsInShop ;i++)
        {

           // int randnumpicked =  Random.Range(0, CopyList.Count);

          //  Upgrade upgrade = CopyList[randnumpicked];

            Upgrade upgrade = FindPool(player.Round)[i];

            //CopyList.RemoveAt(randnumpicked);
            //player.UpgradePoolRound1.RemoveAt(randnumpicked);

            //Actually create it
            GameObject item = Instantiate(UpgradePrefab, shopUiTransform);
            item.name = upgrade.Name;

            upgrade.itemRef = item;
            upgrade.Purchased = false;

            foreach (Transform child in item.transform)
            {
                upgrade.Purchased = false;
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
                else if (child.gameObject.name == "Description")
                {
                    child.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = upgrade.Description;
                }
            }



            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuyUpgrade(upgrade, i);
            });
        }
    }
    

    void BuyUpgrade(Upgrade upgrade, int i)
    {
        if (player.Scrap >= upgrade.Cost)
        {
            player.Scrap -= upgrade.Cost;
            scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap " + player.Scrap;

            //These are for 1 time purchases
            //   FindPool(player.Round)[i].Purchased = true;
            // print(FindPool(player.Round)[i].Name);
            print(upgrade.Name);
            upgrade.Purchased = true;
            upgrade.itemRef.SetActive(false);
            //Take previous weapon and add it to the weapons chest
            if (!player.PastWeapons.Contains(player.curWep))
            {
                player.PastWeapons.Add(player.curWep);
                //weaponsChest.GetComponent<Weapons_Chest>().getStuff(player.curWep);
                weaponsChest.GetComponent<Weapons_Chest>().getStuff(player.curWep);
            }

            ApplyUpgrade(upgrade);
            SB.Buy();
            //  weaponsChest.GetComponent<Weapons_Chest>().resetBox();
           
        }
    }


    void ApplyUpgrade(Upgrade upgrade)
    {
        Upgrade tempWep = new Upgrade(upgrade.Name,0,upgrade.Sprite,upgrade.Description, false);
        switch (upgrade.Name)
        {
            case "Bolt Launcher":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                player.curWep = tempWep;
                //findpool
                //pool.add(Upgraded Bolt Launcher)
                break;
            case "Shotgun":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                player.curWep = tempWep;
                //findpool
                //pool.add(Upgraded Bolt Launcher)
                break;
            case "Plasma Cutter":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = true;
                WPC.AssignWeapon();
                player.curWep = tempWep;
                //findpool
                //pool.add(Upgraded Bolt Launcher)
                break;
            case "Pistol":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                player.curWep = tempWep;
                player.PistolFireRateMod = 1f;
                //findpool
                //pool.add(Upgraded Bolt Launcher)
                break;
            case "Pistol: Tier 2":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                player.PistolFireRateMod = .80f;
                //Add new item
                FindPool(player.Round + 1).Add(new Upgrade("Pistol: Tier 3", 10, gunSprite, "An upgraded pistol that shoots 30% faster and bullets pierce one enemy", false));
                player.curWep = tempWep;
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Pistol");
                weaponsChest.GetComponent<Weapons_Chest>().resetBox();
                //Remove previous versions of pistol
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Pistol");
                break;
            case "Pistol: Tier 3":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                player.PistolFireRateMod = .70f;
                player.curWep = tempWep;
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Pistol");
                //remove lower tiers
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Pistol: Tier 2");
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
                return player.ScrapPoolRound1;
            case 2:
                return player.ScrapPoolRound2;
            case 3:
                return player.ScrapPoolRound3;
            case 4:
                return player.ScrapPoolRound4;
            case 5:
                return player.ScrapPoolRound5;

        }
        return UpgradesList;
    }

    //Only call once shopping is over
  public void EndShopUpdate()
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

        //if round == 3
        //nextPool.removeAll(WeakPerk);
        //ect, ect.
 
    }
}
