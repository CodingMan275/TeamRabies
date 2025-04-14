using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ultamite : MonoBehaviour
{
    public Slider UltSlider;
    public bool CanUlt = false;
    public SaveData player;
    public GameObject EBlast;

    InputController IC;

    public void OnEnable()
    {
        IC = GetComponent<InputController>();
        IC.UltActivate += UltCheck;
    }


    // Start is called before the first frame update
    void Start()
    {
        UltSlider.value = player.UltPerc;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CanUlt = UltSlider.value == UltSlider.maxValue ? true : false;
    }

    void UltCheck()
    {
        if (CanUlt) 
        {
            player.UltPerc = 0;
            UltSlider.value = player.UltPerc;
            StartCoroutine(Electricblast());
            StartCoroutine(player.StatBoost());
        }
    }
    IEnumerator Electricblast()
    {
        EBlast.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        EBlast.SetActive(false);
    }
}
