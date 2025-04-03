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
    int itemNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (player.Round == 1)
            player.PastWeapons.Clear();
        resetBox();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getStuff(Upgrade curWep)
    {

            // int randnumpicked =  Random.Range(0, CopyList.Count);

            //  Upgrade upgrade = CopyList[randnumpicked];

            //Upgrade upgrade = curWep;

            //CopyList.RemoveAt(randnumpicked);
            //player.UpgradePoolRound1.RemoveAt(randnumpicked);

            //Actually create it
            GameObject item = Instantiate(UpgradePrefab, shopUiTransform);

            curWep.itemRef = item;
            curWep.Purchased = false;

            foreach (Transform child in item.transform)
            {
               // upgrade.Purchased = false;
                if (child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Scrap Cost: " + curWep.Cost;
                }
                else if (child.gameObject.name == "Name")
                {
                    child.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = curWep.Name;
                }
                else if (child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = curWep.Sprite;
                }
                else if (child.gameObject.name == "Description")
                {
                    child.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = curWep.Description;
                }
            



            item.GetComponent<Button>().onClick.AddListener(() =>
            {
                ApplyUpgrade(curWep);
            });
            itemNum++;
        }
    }

    public void resetBox()
    { 
            foreach (Transform child in shopUiTransform)
        {
            Destroy(child.gameObject);
        }
        foreach (Upgrade upgrade in player.PastWeapons)
        {
            getStuff(upgrade);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // freeWeapons = player.PastWeapons;
       
     
    }

    void ApplyUpgrade(Upgrade upgrade)
    {
        print(upgrade.Name);
        if (!player.PastWeapons.Contains(player.curWep))
        {
            player.PastWeapons.Add(player.curWep);
            getStuff(player.curWep);
        }

        switch (upgrade.Name)
        {
            case "Bolt Launcher":
                player.Pistol = false;
                player.Bolt_Launcher = true;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                player.curWep = upgrade;
                WPC.AssignWeapon();
                break;
            case "Shotgun":
                player.Pistol = false;
                player.Bolt_Launcher = false;
                player.Shotgun = true;
                player.PlasmaCutter = false;
                player.curWep = upgrade;
                WPC.AssignWeapon();
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
                break;
            case "Pistol: Tier 2":
                player.Pistol = true;
                player.Bolt_Launcher = false;
                player.Shotgun = false;
                player.PlasmaCutter = false;
                player.PistolFireRateMod = .80f;
                player.curWep = upgrade;
                player.PastWeapons.RemoveAll(upgrade => upgrade.Name == "Pistol");
                WPC.AssignWeapon();
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