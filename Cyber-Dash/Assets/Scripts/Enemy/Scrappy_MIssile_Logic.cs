using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrappy_MIssile_Logic : MonoBehaviour
{
    //Spawn all the missiles

    public GameObject missile;
    public int dmg;
    public float spawnRadius = 4.5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Attack()
    {
       
        Instantiate(missile, transform.position, transform.rotation);
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < 4; i++)
            {
            float posX = (int)Random.Range(0, 2) == 1 ? transform.position.x - Random.Range(0.0f, spawnRadius) : transform.position.x + Random.Range(0.0f, spawnRadius);
            float posY = (int)Random.Range(0, 2) == 1 ? transform.position.y - Random.Range(0.0f, spawnRadius) : transform.position.y + Random.Range(0.0f, spawnRadius);
            //  offset.transform.position = new Vector2(posX, posY);
            GameObject miss = Instantiate(missile, new Vector3(posX,posY,0), transform.rotation);
            miss.GetComponent<Scrappy_MIssile>().dmg = dmg;
            yield return new WaitForSeconds(.2f);
            }
            Destroy(gameObject);
        }
}
