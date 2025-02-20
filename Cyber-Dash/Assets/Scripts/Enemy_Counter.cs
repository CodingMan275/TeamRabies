using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Counter : MonoBehaviour
{
    SceneController SC;
    TextMeshProUGUI Text;
    [SerializeField] private int MaxWave;
    [SerializeField] private int Remaining;
    [SerializeField] private float WaitTimer;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Remaining = MaxWave;
        SC = transform.parent.GetComponent<SceneController>();
    }

    void Update()
    {
        Text.text= "Remaining: "+Remaining+" / "+MaxWave; 
        if(Remaining == 0)
            WaitTimer = WaitTimer <= 0 ? 0 : WaitTimer - Time.deltaTime;
        if(WaitTimer==0)
            SC.Victory();
    }

    public void UpdateCounter()
    {
        Remaining = Remaining <= 0 ? 0 : Remaining - 1;
    }
}
