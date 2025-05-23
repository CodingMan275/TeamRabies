using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Energy_Shop_Logic : MonoBehaviour
{

    // Start is called before the first frame update

    public int HowManyItemsInShop = 4;

    public GameObject playerInScene;

    public GameObject scrapDisplay;
    public SaveData player;
    public ShopData Shop;
    public Transform shopUiTransform;
    public GameObject UpgradePrefab;
    public Weapon_Controller WPC;
    public Shop_Interactiob SB;

    //Master list of all perks
    public List<Upgrade> UpgradesList;



    void Start()
    {

        //Reset perk pool
        if(player.Round == 1)
        {
            Shop.PoolRound1 = new List<Upgrade>(Shop.StaticPoolRound1);
            Shop.PoolRound2 = new List<Upgrade>(Shop.StaticPoolRound2);
            Shop.PoolRound3 = new List<Upgrade>(Shop.StaticPoolRound3);
            Shop.PoolRound4 = new List<Upgrade>(Shop.StaticPoolRound4);
            Shop.PoolRound5 = new List<Upgrade>(Shop.StaticPoolRound5);
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

            item.name = upgrade.Name;
            upgrade.Purchased = false;

            upgrade.itemRef = item;

            foreach (Transform child in item.transform)
            {
                if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = upgrade.Sprite;
                    child.gameObject.GetComponent<Image>().preserveAspect = true;
                }
                
                else if (child.gameObject.name == "Description")
                {
                    child.gameObject.GetComponentInChildren<Image>().sprite = upgrade.Description;
                }
                else if (child.gameObject.name == "text")
                {
                    child.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = upgrade.text;
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
        if (player.Energy >= upgrade.Cost)
        {
            player.Energy -= upgrade.Cost;
            scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Energy " + player.Energy;

            //These are for 1 time purchases
            for (int i = 0; i < FindPool(player.Round).Count; i++)
            {
                if (FindPool(player.Round)[i].Name == upgrade.Name)
                {
                    FindPool(player.Round)[i].Purchased = true;
                }
            }
           // FindPool(player.Round)[randnum].Purchased = true;
            upgrade.itemRef.SetActive(false);

            print("Energy shop bought item: " + upgrade.Name);

            ApplyUpgrade(upgrade);
            SB.Buy();
        }
    }


    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.Name)
        {
            case "Rapid Fire Circuit": player.FireRateMod -= .15f;
                break;
            case "Dodge Booster": player.DodgeCooldownMod = 1;
                break;
            case "Explosive Rounds":
                player.explodingBullets = true;
                break;
            case "Turret":
                player.Turret = true;
                player.repairPack = false;
                player.overchargeBattery = false;
                player.injector = false;
                playerInScene.GetComponent<Ability_Turret>().enabled = true;
                playerInScene.GetComponent<Ability_RepairPack>().enabled = false;
                playerInScene.GetComponent<Ability_Overcharge>().enabled = false;
                playerInScene.GetComponent<Ability_Injector>().enabled = false;
                break;
            case "Critical Strike":
                player.criticalStrike = true;
            break;
            case "Scrap Recycle":
                player.scrapRecycle = true;
                break;
            case "Reinforced Chassis":
                player.healthBuff = (int)(player.MaxHealth * .2f);
                break;
            case "Energy Deflector":
                player.energyDeflector = true;
                break;
            case "Adaptive Armor":
                player.adaptiveArmor = true;
                break;
            case "Repair Pack":
                player.Turret = false;
                player.repairPack = true;
                player.overchargeBattery = false;
                player.injector = false;
                playerInScene.GetComponent<Ability_Turret>().enabled = false;
                playerInScene.GetComponent<Ability_RepairPack>().enabled = true;
                playerInScene.GetComponent<Ability_Overcharge>().enabled = false;
                playerInScene.GetComponent<Ability_Injector>().enabled = false;
                break;
            case "Overcharge":
                player.Turret = false;
                player.repairPack = false;
                player.overchargeBattery = true;
                player.injector = false;
                playerInScene.GetComponent<Ability_Turret>().enabled = false;
                playerInScene.GetComponent<Ability_RepairPack>().enabled = false;
                playerInScene.GetComponent<Ability_Overcharge>().enabled = true;
                playerInScene.GetComponent<Ability_Injector>().enabled = false;
                break;
            case "Adrenaline Injector":
                player.Turret = false;
                player.repairPack = false;
                player.overchargeBattery = false;
                player.injector = true;
                playerInScene.GetComponent<Ability_Turret>().enabled = false;
                playerInScene.GetComponent<Ability_RepairPack>().enabled = false;
                playerInScene.GetComponent<Ability_Overcharge>().enabled = false;
                playerInScene.GetComponent<Ability_Injector>().enabled = true;
                break;
            case "Last Stand":
                player.lastStand = true;
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
       scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Energy " + player.Energy;
    }

  List<Upgrade> FindPool(int round)
    {
        switch (round)
        {
            case 1:
                return Shop.PoolRound1;
            case 2:
                return Shop.PoolRound2; 
            case 3:
                return Shop.PoolRound3;
            case 4:
                return Shop.PoolRound4;
            case 5:
                return Shop.PoolRound5;

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

[System.Serializable]
public class Upgrade
{
    public string Name;
    public int Cost;
    public Sprite Sprite;
    public Sprite Description;
    public string text;
    [HideInInspector] public GameObject itemRef;
    public bool Purchased;

    public Upgrade(string Name, int Cost, Sprite Sprite, Sprite Decsription,string text, bool Purchased)
    {
        this.Name = Name;
        this.Cost = Cost;
        this.Sprite = Sprite;
        this.Description = Decsription;
        this.text = text;
        this.Purchased = Purchased;
    }
}
