using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_Overcharge : MonoBehaviour
{
    InputController IC;

    public GameObject abiltiyCountdown;
    public GameObject ultSlider;
    public float cooldown = 30f;


    public Sprite icon;

    float AbilityCooldown;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        abiltiyCountdown.SetActive(true);
        abiltiyCountdown.GetComponent<Slider>().maxValue = cooldown;
        AbilityCooldown = cooldown;
        abiltiyCountdown.GetComponentInChildren<Image>().sprite = icon;
        IC = GetComponent<InputController>();
        IC.OnAbilityPressed += Overcharge;
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

    void Overcharge()
    {
        if (!active)
        {
            GetComponent<Player_Health>().stats.UltPerc += 25;
            if (GetComponent<Player_Health>().stats.UltPerc > 100)
            {
                GetComponent<Player_Health>().stats.UltPerc = 100;
            }
            ultSlider.GetComponent<Slider>().value = GetComponent<Player_Health>().stats.UltPerc;
            active = true;
        }
    }
    private void OnDisable()
    {
        IC.OnAbilityPressed -= Overcharge;
    }
}
