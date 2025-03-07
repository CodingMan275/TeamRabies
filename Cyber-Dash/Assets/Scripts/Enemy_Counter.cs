using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Counter : MonoBehaviour
{
    SceneController SC;
    TextMeshProUGUI Text;
    private int Enemies;
    [SerializeField] private int Remaining = 100;
    [SerializeField] private float WaitTimer;
    public EnemySpawner[] Spawner;
    public GameObject[] E;
    public bool Activate = false;

    // Start is called before the first frame update
    void Start()
    {
        Spawner = FindObjectsOfType<EnemySpawner>();
        E = GameObject.FindGameObjectsWithTag("Enemy"); 
        SC = transform.parent.GetComponent<SceneController>();
        Text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(GetRemaining());
    }

    void Update()
    {
        if(Activate)
        {
        Text.text= "Remaining: "+Remaining+" / "+Enemies; 
        if(Remaining == 0)
            WaitTimer = WaitTimer <= 0 ? 0 : WaitTimer - Time.deltaTime;
        if (WaitTimer == 0)
            SC.Shop();
        }
    }

    public void UpdateCounter()
    {
        Remaining = Remaining <= 0 ? 0 : Remaining - 1;
    }

    IEnumerator GetRemaining()
    {
        yield return new WaitForSeconds(.5f);
        foreach(EnemySpawner e in Spawner)
            Remaining += e.TotalEnemy;
        Remaining += E.Length;
        Enemies = Remaining;
        print("Create Remaining");
        Activate = true;

    }
}
