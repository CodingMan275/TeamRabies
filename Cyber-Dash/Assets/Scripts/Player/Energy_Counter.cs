using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Counter : MonoBehaviour
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

    private void OnGUI()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Energy: " + player.Energy;
    }
}
