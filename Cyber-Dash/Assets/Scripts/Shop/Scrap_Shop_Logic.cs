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
    public ShopData Shop;
    public Transform shopUiTransform;
    public GameObject UpgradePrefab;
    public Weapon_Controller WPC;
    public Shop_Interactiob SB;

    public Sprite gunSprite;
    public Sprite shotgunSprite;
    public Sprite boltSprite;
    public Sprite beamSprite;


    //Master list of all perks
    public List<Upgrade> UpgradesList;
    public List<Sprite> descriptions;

    public GameObject weaponsChest;

    void Start()
    {

        //Reset perk pool
        if(player.Round == 1)
        {
            //Sets the current wepon ALFE is holding, which will then be put in the weapin cheat
            //Upon another being purchased
            player.curWep = new Upgrade("Pistol", 0, gunSprite, descriptions[0],"", false);
            player.PastWeapons.Clear();
            Shop.PoolRound1 = new List<Upgrade>(Shop.StaticPoolRound1);
            Shop.PoolRound2 = new List<Upgrade>(Shop.StaticPoolRound2);
            Shop.PoolRound3 = new List<Upgrade>(Shop.StaticPoolRound3);
            Shop.PoolRound4 = new List<Upgrade>(Shop.StaticPoolRound4);
            Shop.PoolRound5 = new List<Upgrade>(Shop.StaticPoolRound5);
        }

        for(int i = 0; i < HowManyItemsInShop ;i++)
        {
            
            //Take the weapon from spot i
            Upgrade upgrade = FindPool(player.Round)[i];

            //Create a prefab for the UI which is the button, description, ect.
            GameObject item = Instantiate(UpgradePrefab, shopUiTransform);
            //Set the prefab's name so that its easier to see whats happening in debug
            item.name = upgrade.Name;

            //Associate the weapon with the prefab
            upgrade.itemRef = item;
            upgrade.Purchased = false;

            //Go through each part of the prefab and change whats needed
            foreach (Transform child in item.transform)
            {
               if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = upgrade.Sprite;
                    child.gameObject.GetComponent<Image>().preserveAspect = true;
                }
                //Change to have the associated png with it
                else if (child.gameObject.name == "Description")
                {
                    child.gameObject.GetComponentInChildren<Image>().sprite = upgrade.Description;
                }
             
            }


            //Set the button part of the UI prefab to actually do something
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                //sends the weapon info,
                BuyUpgrade(upgrade);
            });
        }
    }
    

    //Do the buying logic
    void BuyUpgrade(Upgrade upgrade)
    {
        //If buyable
        if (player.Scrap >= upgrade.Cost)
        {
            //Subtract cost from funds
            player.Scrap -= upgrade.Cost;
            //Update UI
            scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap " + player.Scrap;

            //These are for 1 time purchases
            //debug testing
            print("Scrap shop bought item: " + upgrade.Name);
            //set purchased to true
            upgrade.Purchased = true;
            //Seting this active to false removes it from the visible UI BUT IT STILL SI THERE IN CHILD COUNT, very tricky
            upgrade.itemRef.SetActive(false);

            //Take previous weapon and add it to a list of past weapons for the eapons chest
            if (!player.PastWeapons.Contains(player.curWep))
            {
                player.PastWeapons.Add(player.curWep);
                //While we are in the scene add it
                weaponsChest.GetComponent<Weapons_Chest>().getStuff(player.curWep);
            }
            //Apply the upgrade/ weapon to our player
            ApplyUpgrade(upgrade);
            //For controller, this functioon keeps the shops working for non mouse events
            SB.Buy();           
        }
    }

    //Taking what upgrade was bought and applying logic to it
    void ApplyUpgrade(Upgrade upgrade)
    {
        //Holding what was bought so that if it gets changed out it has the information it needs
        Upgrade tempWep = new Upgrade(upgrade.Name,0,upgrade.Sprite,upgrade.Description,upgrade.text, false);
        //What was bought?
        switch (upgrade.Name)
        {
            case "Bolt Launcher":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = boltSprite;
                player.curWep = tempWep;
                FindPool(player.Round + 1).Add(new Upgrade("Bolt Launcher: Tier 2", 10, boltSprite, descriptions[7],"", false));

                break;
            case "Shotgun":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = shotgunSprite;
                player.curWep = tempWep;
                player.HowManyPierce = 0;
                player.ShotgunReloadTime = 2;
                FindPool(player.Round + 1).Add(new Upgrade("Shotgun: Tier 2", 100, shotgunSprite, descriptions[4],"", false));
                break;
            case "Plasma Cutter":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = true;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = beamSprite;
                player.curWep = tempWep;
                FindPool(player.Round + 1).Add(new Upgrade("Plasma Cutter: Tier 2", 100, beamSprite, descriptions[10],"", false));
                break;
                //This isn't possible to buy but just in case
            case "Pistol":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                player.curWep = tempWep;
                player.PistolFireRateMod = 1f;
                break;
                //Where it starts getting tricky
                //But the logic is the same for all upgradable weapons
            case "Pistol: Tier 2":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = gunSprite;
                player.PistolFireRateMod = .80f;
                //So now we need to add the next upgrade to this weapon for the next pool of items
                FindPool(player.Round + 1).Add(new Upgrade("Pistol: Tier 3", 100, gunSprite, descriptions[2],"", false));
                player.curWep = tempWep;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Pistol");
                //This remakes the UI elements in the weapons chest, removing the lesser version
                weaponsChest.GetComponent<Weapons_Chest>().resetBox();
                //Remove previous versions of pistol from the next pool
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Pistol");
                break;
            case "Pistol: Tier 3":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = gunSprite;
                player.PistolFireRateMod = .70f;
                player.HowManyPierce = 1;
                player.curWep = tempWep;
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Pistol: Tier 2");
                //remove lower tiers
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Pistol: Tier 2");
                break;
            case "Shotgun: Tier 2":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = shotgunSprite;
                player.HowManyPierce = 0;
                player.ShotGunAmmo = 4;
                player.ShotgunReloadTime = 1.5f;
                player.curWep = tempWep;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Shotgun");
                //This remakes the UI elements in the weapons chest, removing the lesser version
                weaponsChest.GetComponent<Weapons_Chest>().resetBox();
                //Remove previous versions of pistol from the next pool
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Shotgun");
                //Add next shotgun
                FindPool(player.Round + 1).Add(new Upgrade("Shotgun: Tier 3", 100, shotgunSprite, descriptions[5],"", false));
                break;
            case "Shotgun: Tier 3":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = shotgunSprite;
                player.HowManyPierce = 0;
                player.ShotGunAmmo = 6;
                player.ShotgunReloadTime = 1.2f;
                player.curWep = tempWep;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Shotgun: Tier 2");
                //This remakes the UI elements in the weapons chest, removing the lesser version
                weaponsChest.GetComponent<Weapons_Chest>().resetBox();
                //Remove previous versions of pistol from the next pool
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Shotgun: Tier 2");
                break;
            case "Bolt Launcher: Tier 2":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = boltSprite;
                player.boltIncreaseDamage = 1;
                player.boltSpeedIncrease = 7;
                player.curWep = tempWep;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Bolt Launcher");
                //This remakes the UI elements in the weapons chest, removing the lesser version
                weaponsChest.GetComponent<Weapons_Chest>().resetBox();
                //Remove previous versions of pistol from the next pool
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Bolt Launcher");
                //Add next bolt launcher
                FindPool(player.Round + 1).Add(new Upgrade("Bolt Launcher: Tier 3", 100, boltSprite, descriptions[8],"", false));
                break;
            case "Boltlauncher: Tier 3":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = shotgunSprite;
                player.boltIncreaseDamage = 2;
                player.boltSpeedIncrease = 14;
                player.curWep = tempWep;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Bolt Launcher: Tier 2");
                //This remakes the UI elements in the weapons chest, removing the lesser version
                weaponsChest.GetComponent<Weapons_Chest>().resetBox();
                //Remove previous versions of pistol from the next pool
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Bolt Launcher: Tier 2");
                break;
            case "Plasma Cutter: Tier 2":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = true;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = beamSprite;
                player.beamCharge = .3f;
                player.beamDamage = 8;
                //So now we need to add the next upgrade to this weapon for the next pool of items
                FindPool(player.Round + 1).Add(new Upgrade("Plasma Cutter: Tier 3", 10, beamSprite, descriptions[11],"", false));
                player.curWep = tempWep;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Plasma Cutter");
                //This remakes the UI elements in the weapons chest, removing the lesser version
                weaponsChest.GetComponent<Weapons_Chest>().resetBox();
                //Remove previous versions of pistol from the next pool
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Plasma Cutter");
                break;
            case "Plasma Cutter: Tier 3":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = true;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = beamSprite;
                player.beamCharge = .5f;
                player.beamDamage = 10;
                //So now we need to add the next upgrade to this weapon for the next pool of items
                player.curWep = tempWep;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Plasma Cutter: Tier 2");
                //This remakes the UI elements in the weapons chest, removing the lesser version
                weaponsChest.GetComponent<Weapons_Chest>().resetBox();
                //Remove previous versions of pistol from the next pool
                FindPool(player.Round + 1).RemoveAll(upgrade => upgrade.Name == "Plasma Cutter: Tier 2");
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
    //Updates UI
    private void OnGUI()
    {
       scrapDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap " + player.Scrap;
    }

    //gets the curremt pool of weapons and scrap upgrade
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
    //This will carry over what wasnt bought
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
