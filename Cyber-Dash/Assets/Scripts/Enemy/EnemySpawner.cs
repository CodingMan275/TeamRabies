using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public int[] EnemyCounter;
    private int SpawnCount;

    public bool StartSummon = false;

    private int[] CounterControl;
    [Tooltip("Time between each spawn.")] public float SpawnTimer;
    [Tooltip("Time between each summon.")]public float SummonTimer;
    [Tooltip("Amount of times a spawner can spawn.")] public int SummonCounter;
    public float SpawnRadius;
    private float NextSpawn;
    private bool Respawn = true;
    private GameObject offset;

    [HideInInspector]public int TotalEnemy;
    // Start is called before the first frame update
    void Start()
    {
        foreach (int i in EnemyCounter)
            SpawnCount += i;
        TotalEnemy = SpawnCount * SummonCounter;
        print("Create Total");
        NextSpawn = SpawnTimer;
        offset = transform.GetChild(0).GameObject();
        if (StartSummon)
        {
            CounterControl = (int[])EnemyCounter.Clone();
            Respawn = false;
            StartCoroutine(Summon());
        }

    }

    // Update is called once per frame
    void Update()
    {
        NextSpawn = Respawn ? NextSpawn - Time.deltaTime : SpawnTimer;
        if (NextSpawn <= 0)
        {
            CounterControl = (int[])EnemyCounter.Clone();
            Respawn = false;
            StartCoroutine(Summon());
        }
    }

    IEnumerator Summon()
    {
        SummonCounter--;

        yield return new WaitForSeconds(.5f);
        Respawn = SummonCounter == 0 ? false : true;
        for (int i = 0; i < SpawnCount; i++)
        {
            int p;
            bool Picked = false;
            do
            {
                p = (int)Random.Range(0, EnemyCounter.Length);
                if (CounterControl[p] > 0)
                    Picked = true;

            } 
            while (!Picked);

            float posX = (int)Random.Range(0,2) == 1 ? transform.position.x - Random.Range(0.0f,SpawnRadius) : transform.position.x + Random.Range(0.0f, SpawnRadius);
            float posY = (int)Random.Range(0,2) == 1 ? transform.position.y - Random.Range(0.0f,SpawnRadius) : transform.position.y + Random.Range(0.0f, SpawnRadius);
            offset.transform.position = new Vector2(posX, posY);
            GameObject E = Instantiate(Enemies[p],offset.transform);
            CounterControl[p]--;
            E.transform.SetParent(null);
            yield return new WaitForSeconds(SummonTimer);
        }
    }
}
