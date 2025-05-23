using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Counter : MonoBehaviour
{
    public SceneController SC;
    public InputController IC;
    TextMeshProUGUI Text;
    private int Enemies;
    [SerializeField] private int Remaining = 100;
    public EnemySpawner[] Spawner;
    public GameObject[] E;
    public bool Activate = false;

    private bool playaudio = true;
    public int WaitTime = 1;
    public GameObject AudioUI;
    public AudioSource Victory;
    public AudioClip[] VictorySound;

    public SaveData SD;

    public GameObject Doomba;

    // Start is called before the first frame update
    void Start()
    {
        Spawner = FindObjectsOfType<EnemySpawner>();
        E = GameObject.FindGameObjectsWithTag("Enemy"); 
        Text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(GetRemaining());
    }

    void FixedUpdate()
    {
        IC.CanShoot(Remaining <= 0 ? false : true);
        if(Activate)
        {
        Text.text= "Remaining: "+Remaining+" / "+Enemies; 

        if(Doomba != null && Remaining < 2)
                Doomba.SetActive(true);

        if(Remaining == 0 && playaudio)
            {
                playaudio = false;
            StartCoroutine(VictoryScene());
            }
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
        if (Doomba != null)
            Remaining++;
        Enemies = Remaining;
        Activate = true;
    }

    IEnumerator VictoryScene()
    {
        AudioUI.GetComponent<VolumeController>().Mute(true);
        int voice = Random.Range(0, 2);
        Victory.clip = VictorySound[voice];
        Victory.Play();
        yield return new WaitForSeconds(VictorySound[voice].length);
        yield return new WaitForSeconds(WaitTime - VictorySound[voice].length);
        if (SD.Round != 5)
        {
            SC.ChangeScene("Shop");
        }
        else
        {
            SC.ChangeScene("Victory");
        }
    }
}
