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

    //Master list of all perks
    public List<Upgrade> UpgradesList;

    //Current pool of upgrades
    List<Upgrade> pool;





    void Start()
    {

        //Reset perk pool
        if(player.Round == 1)
        {
            player.ScrapPoolRound1 = new List<Upgrade>(player.StaticScrapPoolRound1);
            player.ScrapPoolRound2 = new List<Upgrade>(player.StaticScrapPoolRound2);
            player.ScrapPoolRound3 = new List<Upgrade>(player.StaticScrapPoolRound3);
            player.ScrapPoolRound4 = new List<Upgrade>(player.StaticScrapPoolRound4);
            player.ScrapPoolRound5 = new List<Upgrade>(player.StaticScrapPoolRound5);
        }

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
                BuyUpgrade(upgrade, randnumpicked);
            });
        }
    }

    void ShowDescription()
    { 

    }

    void HideDescription()
    { 
    
    }

    
    

    void BuyUpgrade(Upgrade upgrade, int randnum)
    {
        if (player.Scrap >= upgrade.Cost)
        {
            player.Scrap -= upgrade.Cost;
            scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap " + player.Scrap;

            //These are for 1 time purchases
            FindPool(player.Round)[randnum].Purchased = true;
            upgrade.itemRef.SetActive(false);

            ApplyUpgrade(upgrade);
            SB.Buy();
        }
    }


    void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.Name)
        {
            case "Bolt Launcher":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.Sword = false;
                WPC.AssignWeapon();
                //findpool
                //pool.add(Upgraded Bolt Launcher)
                break;
            case "Shotgun":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.Sword = false;
                WPC.AssignWeapon();
                //findpool
                //pool.add(Upgraded Bolt Launcher)
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
    /*
    List<Upgrade> FindStaticPool(int round)
    {
        switch (round)
        {
            case 1:
                return player.StaticUpgradePoolRound1;
            case 2:
                return player.StaticUpgradePoolRound2;
            case 3:
                return player.StaticUpgradePoolRound3;
            case 4:
                return player.StaticUpgradePoolRound4;
            case 5:
                return player.StaticUpgradePoolRound5;

        }
        return UpgradesList;
    }
    
    */
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
