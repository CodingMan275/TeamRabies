using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_RepairPack : MonoBehaviour
{
    InputController IC;

    public GameObject abiltiyCountdown;
    public float cooldown = 15f;


    public Sprite icon;

    float AbilityCooldown;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        abiltiyCountdown.SetActive(true);
        abiltiyCountdown.GetComponent<Slider>().maxValue = cooldown;
        AbilityCooldown = cooldown;
        // abiltiyCountdown.GetComponentInChildren<Image>().sprite = icon;
        // abiltiyCountdown.transform.GetChild(1).GetComponent<Image>().sprite = icon;
        foreach (Transform child in abiltiyCountdown.transform)
        {
            if (child.gameObject.name == "Image")
            {
                child.gameObject.GetComponent<Image>().sprite = icon;
            }
        }
        IC = GetComponent<InputController>();
        IC.OnAbilityPressed += Repair;
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
                AbilityCooldown = cooldown;
            }
        }
    }

    void Repair()
    {
        if (!active)
        {
            GetComponent<Player_Health>().stats.Health += 15;
            if (GetComponent<Player_Health>().stats.Health > GetComponent<Player_Health>().stats.MaxHealth)
            {
                GetComponent<Player_Health>().stats.Health = GetComponent<Player_Health>().stats.MaxHealth;
            }
            GetComponent<Player_Health>().HealthBar.value = GetComponent<Player_Health>().stats.Health;
            active = true;
        }
    }
    private void OnDisable()
    {
        IC.OnAbilityPressed -= Repair;
    }
}
