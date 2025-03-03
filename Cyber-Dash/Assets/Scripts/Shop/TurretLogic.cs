using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{


    public SaveData enemies;
    float angle;

    public GameObject BulletPrefab;
    public GameObject Offset;

    public float BulletSpeed;
    public double BulletDelay;
    float LastTimeBulletFired;
    float timeSinceLastFiredBullet;

    public float DeployTime = 10f;

    public List<GameObject> EnemyList;
    GameObject closestObject = null;

    Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       timeSinceLastFiredBullet = Time.time - LastTimeBulletFired;
       DeployTime -= Time.deltaTime;
        if (DeployTime <= 0)
        {
            Destroy(gameObject);
        }
       
    }

    private void FixedUpdate()
    {
         if (closestObject == null && (EnemyList.Count != 0))
        {
            GetClosestObject();
        }
        else 
        {
            if (closestObject != null)
           {
                Vector3 dir = closestObject.transform.position - transform.position;
                Direction = new Vector3(closestObject.transform.position.x - transform.position.x, closestObject.transform.position.y - transform.position.y);
                transform.up = Direction;
                Fire();
            }
        }
    }

    public GameObject GetClosestObject()
    {
        float closest = 6000; //add your max range here
        
        for (int i = 0; i < EnemyList.Count; i++)  //list of gameObjects to search through
        {
            float dist = Vector3.Distance(EnemyList[i].transform.position, gameObject.transform.position);
            if (dist < closest)
            {
                closest = dist;
                closestObject = EnemyList[i];
            }
        }
        return closestObject;
    }


    public void Fire()
    { 
    if ((timeSinceLastFiredBullet > BulletDelay))
        {
            print("FIRE");
            GameObject Bullet = Instantiate(BulletPrefab, Offset.transform.position, transform.rotation);
            Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed, ForceMode2D.Impulse);
            LastTimeBulletFired = Time.time;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            EnemyList.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            EnemyList.Remove(collision.gameObject);
    }




}
