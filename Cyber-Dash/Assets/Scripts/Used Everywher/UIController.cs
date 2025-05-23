using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public PlayerControl pc;
    public InputController IC;
    public GameObject PauseUI;
    public GameObject PlayerUI;
    public GameObject AudioUI;
    private InputAction Pause;

    private GameObject ExitButotn;

    public event Action <bool>OnPausePressed;

    public bool Paused = false;


    private void OnEnable()
    {
        Pause = pc.UISystem.Pause;
        Pause.Enable();
        OnPausePressed += PauseGame;
    }

    private void OnDisable()
    {
        Pause.Disable();
    }

    private void Awake()
    {
        AudioUI.GetComponent<VolumeController>().SetVolume();
        pc = new PlayerControl();
    }


    // Update is called once per frame
    void Update()
    {
        if (Pause.triggered)
            OnPause();
    }

    public void OnPause()
    {
            OnPausePressed?.Invoke(!Paused);
            AudioUI.GetComponent<VolumeController>().Mute(PauseUI.active);
        if(PauseUI.active && !AudioUI.active)
        {
            PauseButton();
        }
    }

    public void PauseGame(bool p)
    {
        Paused = p;
        PauseUI.SetActive(p);
        PlayerUI.SetActive(!p);
        if (p)
            GetNewButton(PauseUI.transform.GetChild(1).transform.GetChild(0).gameObject);
        else
        {
            // Force hides the UI and Forces the buttons to be visible
            PauseUI.transform.GetChild(1).gameObject.SetActive(true);
            PauseUI.transform.GetChild(2).gameObject.SetActive(false);
            PauseUI.transform.GetChild(3).gameObject.SetActive(false);
        }
        IC.enabled = !p;
        Time.timeScale = Paused? 0 : 1;
    }

    public void GetNewButton(GameObject G)
    {
        if (IC.Controller)
        {
            IC.EV.firstSelectedGameObject = G;
            IC.EV.firstSelectedGameObject.GetComponent<Button>().Select();
        }
    }

    public void GetNewSlider(GameObject G)
    {
        if (IC.Controller)
        {
            IC.EV.firstSelectedGameObject = G;
            IC.EV.firstSelectedGameObject.GetComponent<Slider>().Select();
        }
    }

    public void GetNewSCrollBar(GameObject G)
    {
        if (IC.Controller)
        {
            IC.EV.firstSelectedGameObject = G;
            IC.EV.firstSelectedGameObject.GetComponent<Scrollbar>().Select();
        }
    }

    public void PauseButton()
    {
            if (IC.Controller)
            {
                IC.EV.firstSelectedGameObject = transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).gameObject;
                IC.EV.firstSelectedGameObject.GetComponent<Button>().Select();
            }
        
    }

    public void exitB(GameObject g)
    {
        ExitButotn = g;
    }

    public void ExitInstruct(Scrollbar S)
    {
        ExitButotn.SetActive(S.value < .05f ? true : false);
    }

}
