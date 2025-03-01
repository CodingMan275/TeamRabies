using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Interactiob : MonoBehaviour
{
    public GameObject shop;
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
            shop.GetComponentInChildren<Canvas>().enabled = true;
           // shop.GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,255f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            shop.GetComponentInChildren<Canvas>().enabled = false;
        }
    }
}
