using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_Injector : MonoBehaviour
{
    InputController IC;
    public float cooldown = 10f;

    public GameObject abiltiyCountdown;

    public Sprite icon;

    float AbilityCooldown;
    bool active = false;
    bool waitbool = false;
    // Start is called before the first frame update
    void Start()
    {
        abiltiyCountdown.SetActive(true);
        abiltiyCountdown.GetComponent<Slider>().maxValue = cooldown;
        AbilityCooldown = cooldown;
        //abiltiyCountdown.GetComponentInChildren<Image>().sprite = icon;
        //  abiltiyCountdown.transform.GetChild(1).GetComponent<Image>().sprite = icon;

        foreach (Transform child in abiltiyCountdown.transform)
        {
            if (child.gameObject.name == "Image")
            {
                child.gameObject.GetComponent<Image>().sprite = icon;
            }
        }
            IC = GetComponent<InputController>();
        IC.OnAbilityPressed += inject;
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
                waitbool = false;
                AbilityCooldown = cooldown;
            }
        }
    }

    void inject()
    {
        if (!active && !waitbool)
        {
            abiltiyCountdown.GetComponent<Slider>().value = abiltiyCountdown.GetComponent<Slider>().maxValue;
            GetComponent<Player_Controller>().SD.FireRateMod *= .75f;
            GetComponent<Player_Controller>().WalkSpeed = 18;
            GetComponent<Player_Controller>().RunSpeed = 23;
            waitbool = true;
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(8f);
        GetComponent<Player_Controller>().SD.FireRateMod /= .75f;
        GetComponent<Player_Controller>().WalkSpeed = 15;
        GetComponent<Player_Controller>().RunSpeed = 20;
        active = true;
    }

    private void OnDisable()
    {
        IC.OnAbilityPressed -= inject;
    }
}
