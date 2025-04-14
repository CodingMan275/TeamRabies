using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy_Drop : MonoBehaviour
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
        if (collision.gameObject.tag == "Player")
        {
            int energy = Random.Range(2,6);
            player.Energy += energy;
            player.UltPerc += energy;
            collision.gameObject.GetComponent<Ultamite>().UltSlider.value += energy; 
            //player.Scrap += rand(minBound, maxBound) * player.ScrapDropMod;
            Destroy(gameObject);
        }
    }
}
