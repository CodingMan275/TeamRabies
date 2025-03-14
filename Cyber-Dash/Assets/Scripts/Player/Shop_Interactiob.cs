using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Shop_Interactiob : MonoBehaviour
{
    public Weapon_Controller WPC;
    public GameObject[] Panel;
     
  
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Shops = GameObject.FindGameObjectsWithTag("Shop");
        if (Gamepad.all.Count > 0)
        {
            
            foreach (GameObject s in Shops)
                s.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            collision.GetComponentInChildren<Canvas>().enabled = true;
            WPC.CanFire = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            collision.GetComponentInChildren<Canvas>().enabled = false;
      
            WPC.CanFire = true;
        }
    }
}
