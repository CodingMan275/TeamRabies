using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop_Interactiob : MonoBehaviour
{
    public Weapon_Controller WPC;
    public EventSystem EV;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            EV.firstSelectedGameObject = collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            collision.GetComponentInChildren<Canvas>().enabled = true;
            WPC.CanFire = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            EV.firstSelectedGameObject = null;
            collision.GetComponentInChildren<Canvas>().enabled = false;
           WPC.CanFire = true;
        }
    }
}
