using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy_Shop_Logic : MonoBehaviour
{

    // Start is called before the first frame update

    public int HowManyItemsInShop = 4;

    public GameObject playerInScene;

    public GameObject scrapDisplay;
    public SaveData player;
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
            player.EnergyPoolRound1 = new List<Upgrade>(player.StaticEnergyPoolRound1);
            player.EnergyPoolRound2 = new List<Upgrade>(player.StaticEnergyPoolRound2);
            player.EnergyPoolRound3 = new List<Upgrade>(player.StaticEnergyPoolRound3);
            player.EnergyPoolRound4 = new List<Upgrade>(player.StaticEnergyPoolRound4);
            player.EnergyPoolRound5 = new List<Upgrade>(player.StaticEnergyPoolRound5);
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
            FindPool(player.Round)[randnum].Purchased = true;
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
                playerInScene.GetComponent<Ability_Turret>().enabled = true;
                playerInScene.GetComponent<Ability_RepairPack>().enabled = false;
                break;
            case "Critical Strike":
                player.criticalStrike = true;
            break;
            case "Scrap Recycle":
                player.scrapRecycle = true;
                break;
            case "Reinforced Chassis":
                player.healthBuff = 10;
                player.Health += 10;
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
                playerInScene.GetComponent<Ability_Turret>().enabled = false;
                playerInScene.GetComponent<Ability_RepairPack>().enabled = true;
                break;
            case "Overcharge":
                player.Turret = false;
                player.repairPack = false;
                player.overchargeBattery = true;
                playerInScene.GetComponent<Ability_Turret>().enabled = false;
                playerInScene.GetComponent<Ability_RepairPack>().enabled = true;
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
                return player.EnergyPoolRound1;
            case 2:
                return player.EnergyPoolRound2;
            case 3:
                return player.EnergyPoolRound3;
            case 4:
                return player.EnergyPoolRound4;
            case 5:
                return player.EnergyPoolRound5;

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
    [HideInInspector] public GameObject itemRef;
    public bool Purchased;

    public Upgrade(string Name, int Cost, Sprite Sprite, Sprite Decsription, bool Purchased)
    {
        this.Name = Name;
        this.Cost = Cost;
        this.Sprite = Sprite;
        this.Description = Decsription;
        this.Purchased = Purchased;
    }
}
