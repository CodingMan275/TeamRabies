using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons_Chest : MonoBehaviour
{
    public SaveData player;

    public GameObject UpgradePrefab;
    public Transform shopUiTransform;
    public Weapon_Controller WPC;
    public Shop_Interactiob SI;
    // Start is called before the first frame update
    void Start()
    {
        //Clear anything from the past
        if (player.Round == 1)
            player.PastWeapons.Clear();
        SI.size = 0;
        //this is a tad redundent but good practice
        resetBox();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getStuff(Upgrade curWep)
    {
        //This function will add past weapons to the UI
        //Either on start or when a weapon has been bought

        //Creat the UI prefab
        GameObject item = Instantiate(UpgradePrefab, shopUiTransform);
        //For debugging make its name the name of the weapon being added
            item.name = curWep.Name;

        //Give the weapon being added a connection to the UI element
              curWep.itemRef = item;
            
        //Go through each child of the UI and update it so it says the correct thing
            foreach (Transform child in item.transform)
            {
               //There is no cost so set this to basically empty
               if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = curWep.Sprite;
                child.gameObject.GetComponent<Image>().preserveAspect = true;
            }
               /*
                else if (child.gameObject.name == "Description")
                {
                    child.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = curWep.Description;
                }
               */
            
                //Give the UI button functionality
            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                ApplyUpgrade(curWep);
            });
        }

        //Functionality for controller
        //Changes the size for every added gun
        SI.size++;
    }

    //At start or specific times remake the UI elements
    //TO make them up to date
    public void resetBox()
    {
        //SI.size = 0;
            foreach (Transform child in shopUiTransform)
        {
            Destroy(child.gameObject);
        }
        foreach (Upgrade upgrade in player.PastWeapons)
        {
            getStuff(upgrade);
        }
    }

    //The weapon button that was pressed lets do the logic
    void ApplyUpgrade(Upgrade upgrade)
    {
        //debugging
        print("Weapon chest:" + upgrade.Name);
        //Lets swap out our current weapon if it is not already in the chest
        if (!player.PastWeapons.Contains(player.curWep))
        {
            //Adding the weapon, ie. currently has the shotgun but wants the pistol. Takes pistol and adds the shotgun
            player.PastWeapons.Add(player.curWep);
            getStuff(player.curWep);
        }

        //what weapon button was pressed
        switch (upgrade.Name)
        {
            case "Bolt Launcher":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                player.curWep = upgrade;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                break;
            case "Shotgun":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.PlasmaCutter = false;
                player.curWep = upgrade;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                break;
            case "Plasma Cutter":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = true;
                WPC.AssignWeapon();
                player.curWep = upgrade;
                break;
            case "Pistol":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                player.curWep = upgrade;
                player.PistolFireRateMod = 1f;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                break;
            case "Pistol: Tier 2":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                player.PistolFireRateMod = .80f;
                player.curWep = upgrade;
                //This line of code should never do anything but it is a fail safe
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Pistol");
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                break;
            case "Pistol: Tier 3":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                player.PistolFireRateMod = .70f;
                player.curWep = upgrade;
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Pistol");
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                break;
            case "Shotgun: Tier 2":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                player.HowManyPierce = 0;
                player.ShotGunAmmo = 4;
                player.ShotgunReloadTime = 1.5f;
                //So now we need to add the next upgrade to this weapon for the next pool of items
                player.curWep = upgrade;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Shotgun");
                break;
            case "Shotgun: Tier 3":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                player.HowManyPierce = 0;
                player.ShotGunAmmo = 6;
                player.ShotgunReloadTime = 1.2f;
                player.curWep = upgrade;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Shotgun: Tier 2");
                break;
            case "Bolt Launcher: Tier 2":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                player.boltIncreaseDamage = 1;
                player.boltSpeedIncrease = 7;
                //So now we need to add the next upgrade to this weapon for the next pool of items
                player.curWep = upgrade;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Bolt Launcher");
                break;
            case "Bolt Launcher: Tier 3":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                player.boltIncreaseDamage = 2;
                player.boltSpeedIncrease = 14;
                player.curWep = upgrade;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Bolt Launcher: Tier 2");
                break;
            case "Plasma Cutter: Tier 2":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = true;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                player.beamCharge = .3f;
                player.beamDamage = 8;
                player.curWep = upgrade;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Plasma Cutter");
                break;
            case "Plasma Cutter: Tier 3":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = true;
                WPC.AssignWeapon();
                WPC.GetComponent<SpriteRenderer>().sprite = upgrade.Sprite;
                player.beamCharge = .5f;
                player.beamDamage = 10;
                //So now we need to add the next upgrade to this weapon for the next pool of items
                player.curWep = upgrade;
                //Go through the past weapons and remove the weaker versions
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Plasma Cutter: Tier 2");
                break;
            default:
                Debug.Log("What did you just do");
                //Example code
                //List<Upgrade> ListRef = FindPool
                //List.Ref.Add(new Upgrade(SwagPerk, 5, SwagIcon.png))

                break;
        }
    }
}