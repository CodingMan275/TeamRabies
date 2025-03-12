using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShop : MonoBehaviour
{

    public SceneController SC;
    public SaveData SD;
    public Energy_Shop_Logic ESL;
    public Scrap_Shop_Logic SSL;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            ESL.EndShopUpdate();
            SSL.EndShopUpdate();
            print("Entered");
            SD.Round++;
            if (SD.Round == 2)
            {
                SC.Arena2();
            }
           // SC.StartGame();
        }
    }
}
