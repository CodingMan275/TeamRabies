using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDescription : MonoBehaviour
{

    public Image i;


    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = false;
        i.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        i.enabled = true;
        GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = true;
    }

    public void Hide()
    {
        i.enabled = false;
        GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = false;
    }
}
