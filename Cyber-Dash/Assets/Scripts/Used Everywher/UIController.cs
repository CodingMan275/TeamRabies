using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public PlayerControl pc;
    public InputController IC;
    public GameObject PauseUI;
    public GameObject AudioUI;
    private InputAction Pause;

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
        pc = new PlayerControl();
    }


    // Start is called before the first frame update
    void Start()
    {
        AudioUI.GetComponent<VolumeSettings>().SetVolume();
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
            AudioUI.GetComponent<VolumeSettings>().Mute(PauseUI.active);
        if(PauseUI.active && !AudioUI.active)
        {
            PauseButton();
        }
    }

    void PauseGame(bool p)
    {
        Paused = p;
        PauseUI.SetActive(p);
        if(!PauseUI.active)
            AudioUI.SetActive(false);
        IC.enabled = !p;
        Time.timeScale = Paused? 0 : 1;
    }

    public void AudioSlider()
    {
        if(IC.Controller)
        {
            IC.EV.firstSelectedGameObject = transform.GetChild(1).transform.GetChild(3).transform.GetChild(2).gameObject;
            IC.EV.firstSelectedGameObject.GetComponent<Slider>().Select();
        }
    }

    public void PauseButton()
    {
            if (IC.Controller)
            {
                IC.EV.firstSelectedGameObject = transform.GetChild(1).transform.GetChild(1).gameObject;
                IC.EV.firstSelectedGameObject.GetComponent<Button>().Select();
            }
        
    }
}
