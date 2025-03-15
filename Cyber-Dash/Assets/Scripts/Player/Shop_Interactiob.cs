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
    public GameObject[] E;
    public EventSystem EV;
    public InputController IC;
    GameObject Shop;
    public int shopPos = 0;
    public int size = 0;

    bool SoldOut = false;

    public void Start()
    {
        EV = E[1].GetComponent<EventSystem>();
    }



    public void Update()
    {
       if(Gamepad.all.Count > 0)
        {
            E[0].SetActive(false);
            E[1].SetActive(true);
        }
        else
        {
            E[0].SetActive(true);
            E[1].SetActive(false);
        }
    }


    private void Rotate(float f)
    {
        bool select = false;
        do
        {
            shopPos += (int)f;
            shopPos = shopPos < 0 ? size-1 : (shopPos == size ? 0 : shopPos);
            if(Shop.transform.GetChild(shopPos).gameObject.active)
            {
                    EV.firstSelectedGameObject = Shop.transform.GetChild(shopPos).gameObject;
                    EV.firstSelectedGameObject.GetComponent<Button>().Select();
                select = true;
            }

        } while (!select);
    }

    public void Buy()
    {
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
        if (collision.gameObject.tag == "Shop")
        {
            if(Gamepad.all.Count > 0 && !SoldOut)
            {
                size = 0;
                 IC.OnShopRotate += Rotate;
                Shop = collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
                bool found = false;
                foreach (Transform child in Shop.transform)
                {
                    size++;
                    if(child.gameObject.active && !found)
                    {
                        EV.firstSelectedGameObject = child.gameObject;
                        found = true;
                    }
                }
                EV.firstSelectedGameObject.GetComponent<Button>().Select();
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
                Shop = null;
                IC.OnShopRotate -= Rotate;
                EV.firstSelectedGameObject = null;
            }
            collision.GetComponentInChildren<Canvas>().enabled = false;
            WPC.CanFire = true;
        }
    }
}
