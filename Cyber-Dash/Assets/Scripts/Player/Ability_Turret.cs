using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_Turret : MonoBehaviour
{
    InputController IC;
    public GameObject TurretPrefab;

    public GameObject abiltiyCountdown;

    public Sprite icon;

    float AbilityCooldown;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        abiltiyCountdown.SetActive(true);
        abiltiyCountdown.GetComponent<Slider>().maxValue = TurretPrefab.GetComponent<TurretLogic>().DeployTime;
        AbilityCooldown = TurretPrefab.GetComponent<TurretLogic>().DeployTime;
        //  abiltiyCountdown.GetComponentInChildren<Image>().sprite = icon;
      //  abiltiyCountdown.transform.GetChild(1).GetComponent<Image>().sprite = icon;
        foreach (Transform child in abiltiyCountdown.transform)
        {
            if (child.gameObject.name == "Image")
            {
                child.gameObject.GetComponent<Image>().sprite = icon;
            }
        }
        IC = GetComponent<InputController>();
        IC.OnAbilityPressed += DeployTurret;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            abiltiyCountdown.GetComponent<Slider>().value = AbilityCooldown;
            abiltiyCountdown.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + ((int)AbilityCooldown + 1);
            if (AbilityCooldown <= .02)
            {
                abiltiyCountdown.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
            }
            AbilityCooldown -= Time.deltaTime;

            if (AbilityCooldown <= 0)
            {
                active = false;
                AbilityCooldown = TurretPrefab.GetComponent<TurretLogic>().DeployTime;
            }
        }

    }

    void DeployTurret()
    {
        if (!active)
        {
            abiltiyCountdown.GetComponent<Slider>().maxValue = TurretPrefab.GetComponent<TurretLogic>().DeployTime;
            Instantiate(TurretPrefab, transform.position, Quaternion.identity);
            active = true;
        }
    }

    private void OnDisable()
    {
        IC.OnAbilityPressed -= DeployTurret;
    }
}
