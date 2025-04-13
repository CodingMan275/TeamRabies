using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shop_Interactiob : MonoBehaviour
{
    public Weapon_Controller WPC;
    public InputController IC;
    public GameObject Shop;
    public int shopPos = 0;
    public int size = 0;
    public AudioSource ShopBell;

    bool SoldOut = false;





    private void Rotate(float f)
    {
        bool select = false;
        do
        {
            shopPos += (int)f;
            shopPos = shopPos < 0 ? size-1 : (shopPos == size ? 0 : shopPos);
            if(Shop.transform.GetChild(shopPos).gameObject.active)
            {
                    IC.EV.firstSelectedGameObject = Shop.transform.GetChild(shopPos).gameObject;
                    IC.EV.firstSelectedGameObject.GetComponent<Button>().Select();
                select = true;
            }

        } while (!select);
    }

    public void Buy()
    {
        ShopBell.Play();
        int Detection = 0;
        foreach (Transform child in Shop.transform)
        {
            if (!child.gameObject.active)
                Detection++;

        }

        if (Detection < size)
            Rotate(1.0f);
        if (Detection >= size)
            SoldOut = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop" || collision.gameObject.tag == "Chest")
        {
            Shop = collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
            Shop.SetActive(true);
            //Unclear, but the buying all the upgrades/ shop seem to be caused by what was commented out.
            if(Gamepad.all.Count > 0)// && !SoldOut)
            {
                size = 0;
                 IC.OnShopRotate += Rotate;
                bool found = false;
                foreach (Transform child in Shop.transform)
                {
                    size++;
                    if(child.gameObject.active && !found)
                    {
                        shopPos = size - 1;
                        IC.EV.firstSelectedGameObject = child.gameObject;
                        found = true;
                    }
                }
                if(found)
                IC.EV.firstSelectedGameObject.GetComponent<Button>().Select();
            }

            collision.GetComponentInChildren<Canvas>().enabled = true;
            WPC.CanFire = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            if (Gamepad.all.Count > 0)
            {
                IC.OnShopRotate -= Rotate;
                IC.EV.firstSelectedGameObject = null;
            }
            Shop.SetActive(false);
            Shop = null;
            collision.GetComponentInChildren<Canvas>().enabled = false;
            WPC.CanFire = true;
            
        }
        if (collision.gameObject.tag == "Chest")
        {
            if (Gamepad.all.Count > 0)
            {
                IC.OnShopRotate -= Rotate;
                IC.EV.firstSelectedGameObject = null;
            }
            Shop.SetActive(false);
            Shop = null;
            collision.GetComponentInChildren<Canvas>().enabled = false;
            WPC.CanFire = true;
        }
    }
}
