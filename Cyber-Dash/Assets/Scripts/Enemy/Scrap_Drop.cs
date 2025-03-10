using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap_Drop : MonoBehaviour
{
    public SaveData player;
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
        if(collision.gameObject.tag == "Player")
        {
            player.Scrap++;
            //player.Scrap += rand(minBound, maxBound) * player.ScrapDropMod;
            Destroy(gameObject);
        }
    }

}
